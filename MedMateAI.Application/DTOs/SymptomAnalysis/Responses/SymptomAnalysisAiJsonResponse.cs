using System.Text.Json.Serialization;

namespace MedMateAI.Application.DTOs.SymptomAnalysis.Responses;

public sealed class SymptomAnalysisAiJsonResponse
{
    [JsonPropertyName("symptoms")]
    public List<SymptomAnalysisAiSymptom> Symptoms { get; set; } = [];

    [JsonPropertyName("severityLevel")]
    public string? SeverityLevel { get; set; }

    [JsonPropertyName("isEmergency")]
    public bool IsEmergency { get; set; }

    [JsonPropertyName("recommendedDepartments")]
    public List<SymptomAnalysisAiDepartment> RecommendedDepartments { get; set; } = [];
}
