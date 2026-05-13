using System.Text.Json;
using MedMateAI.Application.DTOs.MedicalDepartments.Requests;
using MedMateAI.Application.DTOs.MedicalDepartments.Responses;
using MedMateAI.Application.IService;
using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Persistence;
using MedMateAI.Domain.Repository;
using Microsoft.Extensions.Caching.Distributed;

namespace MedMateAI.Application.Service;

public sealed class MedicalDepartmentService : IMedicalDepartmentService
{
    private const string AllDepartmentsCacheKey = "medical-departments:all";
    private const string DepartmentCacheKeyPrefix = "medical-departments:";

    private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(5);
    private static readonly DistributedCacheEntryOptions CacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = CacheTtl,
    };

    private readonly IGenericRepository<MedicalDepartment> _medicalDepartmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDistributedCache _cache;

    public MedicalDepartmentService(
        IGenericRepository<MedicalDepartment> medicalDepartmentRepository,
        IUnitOfWork unitOfWork,
        IDistributedCache cache)
    {
        _medicalDepartmentRepository = medicalDepartmentRepository;
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<IReadOnlyList<MedicalDepartmentResponse>> ListMedicalDepartmentsAsync(
        CancellationToken cancellationToken = default)
    {
        var cached = await _cache.GetStringAsync(AllDepartmentsCacheKey, cancellationToken);
        if (!string.IsNullOrWhiteSpace(cached))
        {
            var cachedResponse = JsonSerializer.Deserialize<List<MedicalDepartmentResponse>>(cached);
            if (cachedResponse is not null)
            {
                return cachedResponse;
            }
        }

        var entities = await _medicalDepartmentRepository.GetAllAsync(cancellationToken);

        var response = entities
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.DepartmentName ?? string.Empty, StringComparer.OrdinalIgnoreCase)
            .Select(MapToResponse)
            .ToList();

        await _cache.SetStringAsync(
            AllDepartmentsCacheKey,
            JsonSerializer.Serialize(response),
            CacheOptions,
            cancellationToken);

        return response;
    }

    public async Task<MedicalDepartmentResponse?> GetMedicalDepartmentByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        var cacheKey = GetDepartmentCacheKey(id);
        var cached = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrWhiteSpace(cached))
        {
            var cachedResponse = JsonSerializer.Deserialize<MedicalDepartmentResponse>(cached);
            if (cachedResponse is not null)
            {
                return cachedResponse;
            }
        }

        var entity = await _medicalDepartmentRepository.GetByIdAsync(id, cancellationToken);
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

    public async Task<(bool Succeeded, IEnumerable<string> Errors, MedicalDepartmentResponse? Data)> CreateMedicalDepartmentAsync(
        CreateMedicalDepartmentRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.DepartmentName))
        {
            return (false, new[] { "Department name is required." }, null);
        }

        var entity = new MedicalDepartment
        {
            Id = Guid.NewGuid(),
            DepartmentName = request.DepartmentName.Trim(),
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
        };

        _medicalDepartmentRepository.Add(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await InvalidateDepartmentCachesAsync(entity.Id, cancellationToken);

        return (true, Array.Empty<string>(), MapToResponse(entity));
    }

    public async Task<(bool Succeeded, bool NotFound, IEnumerable<string> Errors, MedicalDepartmentResponse? Data)> UpdateMedicalDepartmentAsync(
        Guid id,
        UpdateMedicalDepartmentRequest request,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return (false, false, new[] { "Invalid medical department id." }, null);
        }

        if (request.DepartmentName is not null && string.IsNullOrWhiteSpace(request.DepartmentName))
        {
            return (false, false, new[] { "Department name cannot be empty when provided." }, null);
        }

        var entity = await _medicalDepartmentRepository.GetByIdAsync(id, cancellationToken);
        if (entity is null || entity.IsDeleted)
        {
            return (false, true, new[] { "Medical department not found." }, null);
        }

        if (request.DepartmentName is not null)
        {
            entity.DepartmentName = request.DepartmentName.Trim();
        }

        if (request.Description is not null)
        {
            entity.Description = request.Description;
        }

        entity.UpdatedAt = DateTime.UtcNow;

        _medicalDepartmentRepository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await InvalidateDepartmentCachesAsync(id, cancellationToken);

        return (true, false, Array.Empty<string>(), MapToResponse(entity));
    }

    public async Task<(bool Succeeded, bool NotFound, IEnumerable<string> Errors)> SoftDeleteMedicalDepartmentAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return (false, false, new[] { "Invalid medical department id." });
        }

        var entity = await _medicalDepartmentRepository.GetByIdAsync(id, cancellationToken);
        if (entity is null || entity.IsDeleted)
        {
            return (false, true, new[] { "Medical department not found." });
        }

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;

        _medicalDepartmentRepository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await InvalidateDepartmentCachesAsync(id, cancellationToken);

        return (true, false, Array.Empty<string>());
    }

    private static MedicalDepartmentResponse MapToResponse(MedicalDepartment entity)
    {
        return new MedicalDepartmentResponse
        {
            Id = entity.Id,
            DepartmentName = entity.DepartmentName,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
    }

    private async Task InvalidateDepartmentCachesAsync(Guid? id, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(AllDepartmentsCacheKey, cancellationToken);

        if (id.HasValue && id.Value != Guid.Empty)
        {
            await _cache.RemoveAsync(GetDepartmentCacheKey(id.Value), cancellationToken);
        }
    }

    private static string GetDepartmentCacheKey(Guid id)
    {
        return $"{DepartmentCacheKeyPrefix}{id}";
    }
}
