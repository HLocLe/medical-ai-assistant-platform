using System.Text.Json;
using MedMateAI.Application.DTOs.AIConfigs.Requests;
using MedMateAI.Application.DTOs.AIConfigs.Responses;
using MedMateAI.Application.DTOs.Common;
using MedMateAI.Application.IService;
using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Persistence;
using MedMateAI.Domain.Repository;
using Microsoft.Extensions.Caching.Distributed;

namespace MedMateAI.Application.Service;

public sealed class AIConfigService : IAIConfigService
{
    private const string ActiveConfigsCacheKey = "ai-configs:active";
    private const string ConfigCacheKeyPrefix = "ai-configs:";
    private const string TaskTypeCacheKeyPrefix = "ai-configs:task-type:";

    private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(5);
    private static readonly DistributedCacheEntryOptions CacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = CacheTtl,
    };

    private readonly IGenericRepository<AISystemConfig> _aiConfigRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDistributedCache _cache;

    public AIConfigService(
        IGenericRepository<AISystemConfig> aiConfigRepository,
        IUnitOfWork unitOfWork,
        IDistributedCache cache)
    {
        _aiConfigRepository = aiConfigRepository;
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<PagedResponse<AIConfigResponse>> ListAIConfigsAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var paged = await _aiConfigRepository.GetPagedAsync(
            pageNumber,
            pageSize,
            x => !x.IsDeleted,
            q => q.OrderBy(x => x.TaskType),
            cancellationToken: cancellationToken);

        return new PagedResponse<AIConfigResponse>
        {
            PageNumber = paged.PageNumber,
            PageSize = paged.PageSize,
            TotalCount = paged.TotalCount,
            TotalPages = paged.TotalPages,
            Items = paged.Items.Select(MapToResponse).ToList(),
        };
    }

    public async Task<IReadOnlyList<AIConfigResponse>> ListActiveAIConfigsAsync(
        CancellationToken cancellationToken = default)
    {
        var cached = await _cache.GetStringAsync(ActiveConfigsCacheKey, cancellationToken);
        if (!string.IsNullOrWhiteSpace(cached))
        {
            var cachedResponse = JsonSerializer.Deserialize<List<AIConfigResponse>>(cached);
            if (cachedResponse is not null)
            {
                return cachedResponse;
            }
        }

        var entities = await _aiConfigRepository.GetAllAsync(cancellationToken);

        var response = entities
            .Where(x => !x.IsDeleted && x.IsActive)
            .OrderBy(x => x.TaskType, StringComparer.OrdinalIgnoreCase)
            .Select(MapToResponse)
            .ToList();

        await _cache.SetStringAsync(
            ActiveConfigsCacheKey,
            JsonSerializer.Serialize(response),
            CacheOptions,
            cancellationToken);

        return response;
    }

    public async Task<AIConfigResponse?> GetAIConfigByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        var cacheKey = GetConfigCacheKey(id);
        var cached = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrWhiteSpace(cached))
        {
            var cachedResponse = JsonSerializer.Deserialize<AIConfigResponse>(cached);
            if (cachedResponse is not null)
            {
                return cachedResponse;
            }
        }

        var entity = await _aiConfigRepository.GetByIdAsync(id, cancellationToken);
        if (entity is null || entity.IsDeleted)
        {
            return null;
        }

        var response = MapToResponse(entity);

        await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(response),
            CacheOptions,
            cancellationToken);

        return response;
    }

    public async Task<AIConfigResponse?> GetActiveAIConfigByTaskTypeAsync(
        string taskType,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(taskType))
        {
            throw new ArgumentException("Task type is required.");
        }

        var normalizedTaskType = taskType.Trim();
        var normalizedTaskTypeKey = NormalizeTaskTypeForCache(normalizedTaskType);
        var taskTypeCacheKey = GetTaskTypeCacheKey(normalizedTaskTypeKey);

        var cached = await _cache.GetStringAsync(taskTypeCacheKey, cancellationToken);
        if (!string.IsNullOrWhiteSpace(cached))
        {
            var cachedResponse = JsonSerializer.Deserialize<AIConfigResponse>(cached);
            if (cachedResponse is not null)
            {
                return cachedResponse;
            }
        }

        var normalizedTaskTypeLower = normalizedTaskType.ToLowerInvariant();
        var entity = await _aiConfigRepository.FirstOrDefaultAsync(
            x => !x.IsDeleted && x.IsActive && x.TaskType.ToLower() == normalizedTaskTypeLower,
            asNoTracking: true,
            cancellationToken);

        if (entity is null)
        {
            return null;
        }

        var response = MapToResponse(entity);
        await _cache.SetStringAsync(
            taskTypeCacheKey,
            JsonSerializer.Serialize(response),
            CacheOptions,
            cancellationToken);

        return response;
    }

    public async Task<AIConfigResponse> CreateAIConfigAsync(
        CreateAIConfigRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new ArgumentException("Request is required.");
        }

        if (string.IsNullOrWhiteSpace(request.TaskType))
        {
            throw new ArgumentException("TaskType is required.");
        }

        if (!IsValidTemperature(request.Temperature))
        {
            throw new ArgumentException("Temperature must be between 0 and 2.");
        }

        if (!IsValidMaxTokens(request.MaxTokens))
        {
            throw new ArgumentException("MaxTokens must be greater than 0 when provided.");
        }

        var taskType = request.TaskType.Trim();
        var taskTypeLower = taskType.ToLowerInvariant();

        var duplicatedConfig = await _aiConfigRepository.FirstOrDefaultAsync(
            x => !x.IsDeleted && x.TaskType.ToLower() == taskTypeLower,
            asNoTracking: true,
            cancellationToken);

        if (duplicatedConfig is not null)
        {
            throw new InvalidOperationException("TaskType already exists.");
        }

        var entity = new AISystemConfig
        {
            Id = Guid.NewGuid(),
            TaskType = taskType,
            SystemPrompt = NormalizeTextAllowEmpty(request.SystemPrompt),
            Model = NormalizeTextAllowEmpty(request.Model),
            Temperature = request.Temperature,
            MaxTokens = request.MaxTokens,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
        };

        _aiConfigRepository.Add(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await InvalidateCachesAsync(entity.Id, null, entity.TaskType, cancellationToken);

        return MapToResponse(entity);
    }

    public async Task<AIConfigResponse?> UpdateAIConfigAsync(
        Guid id,
        UpdateAIConfigRequest request,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        if (request is null)
        {
            throw new ArgumentException("Request is required.");
        }

        var entity = await _aiConfigRepository.GetByIdAsync(id, cancellationToken);
        if (entity is null || entity.IsDeleted)
        {
            return null;
        }

        var oldTaskType = entity.TaskType;

        if (request.TaskType is not null)
        {
            if (string.IsNullOrWhiteSpace(request.TaskType))
            {
                throw new ArgumentException("TaskType cannot be empty when provided.");
            }

            var normalizedTaskType = request.TaskType.Trim();
            if (!string.Equals(entity.TaskType, normalizedTaskType, StringComparison.OrdinalIgnoreCase))
            {
                var normalizedTaskTypeLower = normalizedTaskType.ToLowerInvariant();
                var duplicatedConfig = await _aiConfigRepository.FirstOrDefaultAsync(
                    x => !x.IsDeleted && x.Id != id && x.TaskType.ToLower() == normalizedTaskTypeLower,
                    asNoTracking: true,
                    cancellationToken);

                if (duplicatedConfig is not null)
                {
                    throw new InvalidOperationException("TaskType already exists.");
                }
            }

            entity.TaskType = normalizedTaskType;
        }

        if (request.SystemPrompt is not null)
        {
            entity.SystemPrompt = NormalizeTextAllowEmpty(request.SystemPrompt);
        }

        if (request.Model is not null)
        {
            entity.Model = NormalizeTextAllowEmpty(request.Model);
        }

        if (request.Temperature.HasValue)
        {
            if (!IsValidTemperature(request.Temperature))
            {
                throw new ArgumentException("Temperature must be between 0 and 2.");
            }

            entity.Temperature = request.Temperature;
        }

        if (request.MaxTokens.HasValue)
        {
            if (!IsValidMaxTokens(request.MaxTokens))
            {
                throw new ArgumentException("MaxTokens must be greater than 0 when provided.");
            }

            entity.MaxTokens = request.MaxTokens;
        }

        if (request.IsActive.HasValue)
        {
            entity.IsActive = request.IsActive.Value;
        }

        entity.UpdatedAt = DateTime.UtcNow;

        _aiConfigRepository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await InvalidateCachesAsync(id, oldTaskType, entity.TaskType, cancellationToken);

        return MapToResponse(entity);
    }

    public async Task<AIConfigResponse?> UpdateAIConfigStatusAsync(
        Guid id,
        UpdateAIConfigStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        if (request is null)
        {
            throw new ArgumentException("Request is required.");
        }

        var entity = await _aiConfigRepository.GetByIdAsync(id, cancellationToken);
        if (entity is null || entity.IsDeleted)
        {
            return null;
        }

        entity.IsActive = request.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        _aiConfigRepository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await InvalidateCachesAsync(id, entity.TaskType, entity.TaskType, cancellationToken);

        return MapToResponse(entity);
    }

    public async Task<bool> DeleteAIConfigAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return false;
        }

        var entity = await _aiConfigRepository.GetByIdAsync(id, cancellationToken);
        if (entity is null || entity.IsDeleted)
        {
            return false;
        }

        var oldTaskType = entity.TaskType;
        var utcNow = DateTime.UtcNow;
        entity.IsDeleted = true;
        entity.DeletedAt = utcNow;
        entity.UpdatedAt = utcNow;

        _aiConfigRepository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await InvalidateCachesAsync(id, oldTaskType, entity.TaskType, cancellationToken);

        return true;
    }

    private async Task InvalidateCachesAsync(
        Guid id,
        string? oldTaskType,
        string? newTaskType,
        CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(ActiveConfigsCacheKey, cancellationToken);
        await _cache.RemoveAsync(GetConfigCacheKey(id), cancellationToken);

        var taskTypeCacheKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        if (!string.IsNullOrWhiteSpace(oldTaskType))
        {
            taskTypeCacheKeys.Add(GetTaskTypeCacheKey(NormalizeTaskTypeForCache(oldTaskType)));
        }

        if (!string.IsNullOrWhiteSpace(newTaskType))
        {
            taskTypeCacheKeys.Add(GetTaskTypeCacheKey(NormalizeTaskTypeForCache(newTaskType)));
        }

        foreach (var cacheKey in taskTypeCacheKeys)
        {
            await _cache.RemoveAsync(cacheKey, cancellationToken);
        }
    }

    private static AIConfigResponse MapToResponse(AISystemConfig entity)
    {
        return new AIConfigResponse
        {
            Id = entity.Id,
            TaskType = entity.TaskType,
            SystemPrompt = entity.SystemPrompt,
            Model = entity.Model,
            Temperature = entity.Temperature,
            MaxTokens = entity.MaxTokens,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
    }

    private static bool IsValidTemperature(decimal? temperature)
    {
        return !temperature.HasValue || temperature.Value is >= 0 and <= 2;
    }

    private static bool IsValidMaxTokens(int? maxTokens)
    {
        return !maxTokens.HasValue || maxTokens.Value > 0;
    }

    private static string? NormalizeTextAllowEmpty(string? value)
    {
        return value?.Trim();
    }

    private static string NormalizeTaskTypeForCache(string taskType)
    {
        return taskType.Trim().ToLowerInvariant();
    }

    private static string GetConfigCacheKey(Guid id)
    {
        return $"{ConfigCacheKeyPrefix}{id}";
    }

    private static string GetTaskTypeCacheKey(string normalizedTaskType)
    {
        return $"{TaskTypeCacheKeyPrefix}{normalizedTaskType}";
    }
}
