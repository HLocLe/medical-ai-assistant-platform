using MedMateAI.Application.DTOs.SymptomAnalysis.Responses.MedGemma;

namespace MedMateAI.Application.IService;

public interface IMedGemmaChatService
{
    Task<MedGemmaChatResult> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken = default);
}
