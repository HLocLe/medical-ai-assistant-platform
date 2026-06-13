using MedMateAI.Application.DTOs.Common;
using MedMateAI.Application.DTOs.IcdChapters.Requests;
using MedMateAI.Application.DTOs.IcdChapters.Responses;

namespace MedMateAI.Application.IService;

public interface IIcdChapterService
{
    Task<PagedResponse<IcdChapterResponse>> ListIcdChaptersAsync(
        int pageNumber,
        int pageSize,
        string? search = null,
        CancellationToken cancellationToken = default);

    Task<IcdChapterResponse?> GetIcdChapterByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<(bool Succeeded, IEnumerable<string> Errors, IcdChapterResponse? Data)> CreateIcdChapterAsync(
        CreateIcdChapterRequest request,
        CancellationToken cancellationToken = default);

    Task<(bool Succeeded, IEnumerable<string> Errors, IReadOnlyList<IcdChapterResponse>? Data)> BulkCreateIcdChaptersAsync(
        BulkCreateIcdChaptersRequest request,
        CancellationToken cancellationToken = default);

    Task<(bool Succeeded, bool NotFound, IEnumerable<string> Errors, IcdChapterResponse? Data)> UpdateIcdChapterAsync(
        Guid id,
        UpdateIcdChapterRequest request,
        CancellationToken cancellationToken = default);

    Task<(bool Succeeded, bool NotFound, IEnumerable<string> Errors)> SoftDeleteIcdChapterAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
