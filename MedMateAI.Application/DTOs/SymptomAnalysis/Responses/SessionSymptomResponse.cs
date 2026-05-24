namespace MedMateAI.Application.DTOs.SymptomAnalysis.Responses;

public sealed class SessionSymptomResponse
{
    public Guid Id { get; set; }

    public string? SymptomName { get; set; }

    public double? ConfidenceScore { get; set; }

    public string? ExtractedText { get; set; }
}
