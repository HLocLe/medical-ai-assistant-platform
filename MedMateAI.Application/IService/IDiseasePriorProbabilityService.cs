using MedMateAI.Application.DTOs.Common;
using MedMateAI.Application.DTOs.DiseasePriorProbabilities.Requests;
using MedMateAI.Application.DTOs.DiseasePriorProbabilities.Responses;

namespace MedMateAI.Application.IService;

public interface IDiseasePriorProbabilityService
{
    Task<PagedResponse<DiseasePriorProbabilityResponse>> ListAsync(
        int pageNumber,
        int pageSize,
        string? search = null,
        bool? isActive = null,
        CancellationToken cancellationToken = default);

    Task<DiseasePriorProbabilityResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<(bool Succeeded, IEnumerable<string> Errors, IReadOnlyList<DiseasePriorProbabilityResponse>? Data)> BulkCreateAsync(
        BulkCreateDiseasePriorProbabilitiesRequest request,
        CancellationToken cancellationToken = default);
}
