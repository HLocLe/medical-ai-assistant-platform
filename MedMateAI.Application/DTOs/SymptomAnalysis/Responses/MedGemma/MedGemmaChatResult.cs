namespace MedMateAI.Application.DTOs.SymptomAnalysis.Responses.MedGemma;

public sealed class MedGemmaChatResult
{
    public string Content { get; set; } = string.Empty;

    public string? Model { get; set; }
}
