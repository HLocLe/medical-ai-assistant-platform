using AutoMapper;
using MedMateAI.Application.DTOs.Common;
using MedMateAI.Application.DTOs.DiseasePriorProbabilities.Requests;
using MedMateAI.Application.DTOs.DiseasePriorProbabilities.Responses;
using MedMateAI.Application.IService;
using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Persistence;

namespace MedMateAI.Application.Service;

public sealed class DiseasePriorProbabilityService : IDiseasePriorProbabilityService
{
    private const int MaxIcd10CodeLength = 20;
    private const int MaxDiseaseNameLength = 500;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DiseasePriorProbabilityService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResponse<DiseasePriorProbabilityResponse>> ListAsync(
        int pageNumber,
        int pageSize,
        string? search = null,
        bool? isActive = null,
        CancellationToken cancellationToken = default)
    {
        var searchTerm = string.IsNullOrWhiteSpace(search) ? null : search.Trim().ToLowerInvariant();

        var paged = await _unitOfWork.DiseasePriorProbabilities.GetPagedAsync(
            pageNumber,
            pageSize,
            item => !item.IsDeleted
                && (!isActive.HasValue || item.IsActive == isActive.Value)
                && (searchTerm == null
                    || item.Icd10Code.ToLower().Contains(searchTerm)
                    || (item.DiseaseName != null && item.DiseaseName.ToLower().Contains(searchTerm))),
            query => query.OrderBy(item => item.Icd10Code),
            cancellationToken: cancellationToken);

        return new PagedResponse<DiseasePriorProbabilityResponse>
        {
            PageNumber = paged.PageNumber,
            PageSize = paged.PageSize,
            TotalCount = paged.TotalCount,
            TotalPages = paged.TotalPages,
            Items = paged.Items.Select(item => _mapper.Map<DiseasePriorProbabilityResponse>(item)).ToList(),
        };
    }

    public async Task<DiseasePriorProbabilityResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        var entity = await _unitOfWork.DiseasePriorProbabilities.GetByIdAsync(id, cancellationToken);
        if (entity is null || entity.IsDeleted)
        {
            return null;
        }

        return _mapper.Map<DiseasePriorProbabilityResponse>(entity);
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors, IReadOnlyList<DiseasePriorProbabilityResponse>? Data)> BulkCreateAsync(
        BulkCreateDiseasePriorProbabilitiesRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null || request.Items is null || request.Items.Count == 0)
        {
            return (false, new[] { "Cần ít nhất một bản ghi P(A)." }, null);
        }

        var (preparationErrors, preparedItems) = PrepareBulkItems(request);
        if (preparationErrors.Count > 0)
        {
            return (false, preparationErrors, null);
        }

        var duplicateErrors = GetDuplicateIcd10CodeErrors(preparedItems);
        if (duplicateErrors.Count > 0)
        {
            return (false, duplicateErrors, null);
        }

        var existingErrors = await GetExistingIcd10CodeErrorsAsync(preparedItems, cancellationToken);
        if (existingErrors.Count > 0)
        {
            return (false, existingErrors, null);
        }

        var responses = await PersistBulkItemsAsync(preparedItems, cancellationToken);
        return (true, Array.Empty<string>(), responses);
    }

    private static (List<string> Errors, List<PreparedBulkItem> Items) PrepareBulkItems(
        BulkCreateDiseasePriorProbabilitiesRequest request)
    {
        var errors = new List<string>();
        var preparedItems = new List<PreparedBulkItem>();

        for (var index = 0; index < request.Items.Count; index++)
        {
            var itemRequest = request.Items[index];
            if (itemRequest is null)
            {
                errors.Add($"Items[{index}]: Bản ghi là bắt buộc.");
                continue;
            }

            var fieldErrors = ValidateFields(
                itemRequest.Icd10Code,
                itemRequest.DiseaseName,
                itemRequest.PA);

            foreach (var fieldError in fieldErrors)
            {
                errors.Add($"Items[{index}]: {fieldError}");
            }

            if (fieldErrors.Count > 0)
            {
                continue;
            }

            preparedItems.Add(new PreparedBulkItem(
                index,
                NormalizeIcd10Code(itemRequest.Icd10Code)!,
                NormalizeDiseaseName(itemRequest.DiseaseName),
                itemRequest.PA,
                itemRequest.IsActive));
        }

        return (errors, preparedItems);
    }

    private static List<string> GetDuplicateIcd10CodeErrors(IReadOnlyList<PreparedBulkItem> preparedItems)
    {
        return preparedItems
            .GroupBy(item => item.Icd10Code, StringComparer.OrdinalIgnoreCase)
            .Where(group => group.Count() > 1)
            .Select(group => $"Icd10Code '{group.Key}' bị trùng trong request.")
            .ToList();
    }

    private async Task<List<string>> GetExistingIcd10CodeErrorsAsync(
        IReadOnlyList<PreparedBulkItem> preparedItems,
        CancellationToken cancellationToken)
    {
        var allItems = await _unitOfWork.DiseasePriorProbabilities.GetAllAsync(cancellationToken);
        var existingCodes = allItems
            .Where(item => !item.IsDeleted)
            .Select(item => item.Icd10Code)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        return preparedItems
            .Where(prepared => existingCodes.Contains(prepared.Icd10Code))
            .Select(prepared =>
                $"Items[{prepared.RequestIndex}]: Icd10Code '{prepared.Icd10Code}' đã tồn tại.")
            .ToList();
    }

    private async Task<IReadOnlyList<DiseasePriorProbabilityResponse>> PersistBulkItemsAsync(
        IReadOnlyList<PreparedBulkItem> preparedItems,
        CancellationToken cancellationToken)
    {
        var createdAtUtc = DateTime.UtcNow;
        var entities = preparedItems
            .Select(prepared => new DiseasePriorProbability
            {
                Id = Guid.NewGuid(),
                Icd10Code = prepared.Icd10Code,
                DiseaseName = prepared.DiseaseName,
                PA = prepared.PA,
                IsActive = prepared.IsActive,
                CreatedAt = createdAtUtc,
            })
            .ToList();

        foreach (var entity in entities)
        {
            _unitOfWork.DiseasePriorProbabilities.Add(entity);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entities
            .OrderBy(entity => entity.Icd10Code, StringComparer.OrdinalIgnoreCase)
            .Select(entity => _mapper.Map<DiseasePriorProbabilityResponse>(entity))
            .ToList();
    }

    private static List<string> ValidateFields(string icd10Code, string? diseaseName, double pa)
    {
        var errors = new List<string>();

        var normalizedCode = NormalizeIcd10Code(icd10Code);
        if (normalizedCode is null)
        {
            errors.Add("Icd10Code là bắt buộc.");
        }
        else if (normalizedCode.Length > MaxIcd10CodeLength)
        {
            errors.Add($"Icd10Code tối đa {MaxIcd10CodeLength} ký tự.");
        }

        var normalizedName = NormalizeDiseaseName(diseaseName);
        if (normalizedName is not null && normalizedName.Length > MaxDiseaseNameLength)
        {
            errors.Add($"DiseaseName tối đa {MaxDiseaseNameLength} ký tự.");
        }

        if (double.IsNaN(pa) || double.IsInfinity(pa) || pa <= 0 || pa > 1)
        {
            errors.Add("PA phải nằm trong khoảng (0, 1].");
        }

        return errors;
    }

    private static string? NormalizeIcd10Code(string? icd10Code)
    {
        if (string.IsNullOrWhiteSpace(icd10Code))
        {
            return null;
        }

        return icd10Code.Trim().ToUpperInvariant();
    }

    private static string? NormalizeDiseaseName(string? diseaseName)
    {
        if (string.IsNullOrWhiteSpace(diseaseName))
        {
            return null;
        }

        return diseaseName.Trim();
    }

    private sealed record PreparedBulkItem(
        int RequestIndex,
        string Icd10Code,
        string? DiseaseName,
        double PA,
        bool IsActive);
}
