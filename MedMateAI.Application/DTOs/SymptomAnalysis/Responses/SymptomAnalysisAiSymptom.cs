using System.Text.Json.Serialization;

namespace MedMateAI.Application.DTOs.SymptomAnalysis.Responses;

public sealed class SymptomAnalysisAiSymptom
{
    [JsonPropertyName("symptomName")]
    public string? SymptomName { get; set; }

    [JsonPropertyName("confidenceScore")]
    public double? ConfidenceScore { get; set; }

    [JsonPropertyName("extractedText")]
    public string? ExtractedText { get; set; }
}
