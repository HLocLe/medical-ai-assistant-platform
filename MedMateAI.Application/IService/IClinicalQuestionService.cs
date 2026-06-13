using MedMateAI.Application.DTOs.ClinicalQuestions.Requests;
using MedMateAI.Application.DTOs.ClinicalQuestions.Responses;
using MedMateAI.Application.DTOs.Common;

namespace MedMateAI.Application.IService;

public interface IClinicalQuestionService
{
    Task<PagedResponse<ClinicalQuestionResponse>> ListClinicalQuestionsAsync(
        int pageNumber,
        int pageSize,
        Guid? chapterId = null,
        string? search = null,
        CancellationToken cancellationToken = default);

    Task<ClinicalQuestionResponse?> GetClinicalQuestionByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<(bool Succeeded, IEnumerable<string> Errors, ClinicalQuestionResponse? Data)> CreateClinicalQuestionAsync(
        CreateClinicalQuestionRequest request,
        CancellationToken cancellationToken = default);

    Task<(bool Succeeded, IEnumerable<string> Errors, IReadOnlyList<ClinicalQuestionResponse>? Data)> BulkCreateClinicalQuestionsAsync(
        BulkCreateClinicalQuestionsRequest request,
        CancellationToken cancellationToken = default);

    Task<(bool Succeeded, bool NotFound, IEnumerable<string> Errors, ClinicalQuestionResponse? Data)> UpdateClinicalQuestionAsync(
        Guid id,
        UpdateClinicalQuestionRequest request,
        CancellationToken cancellationToken = default);

    Task<(bool Succeeded, bool NotFound, IEnumerable<string> Errors)> SoftDeleteClinicalQuestionAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
