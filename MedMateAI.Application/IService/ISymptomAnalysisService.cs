using MedMateAI.Application.DTOs.SymptomAnalysis.Requests;
using MedMateAI.Application.DTOs.SymptomAnalysis.Responses;

namespace MedMateAI.Application.IService;

public interface ISymptomAnalysisService
{
    Task<SymptomAnalysisResponse> AnalyzeAsync(
        AnalyzeSymptomsRequest request,
        CancellationToken cancellationToken = default);

    Task<SymptomAnalysisResponse?> GetSessionByIdAsync(
        Guid sessionId,
        CancellationToken cancellationToken = default);
}
