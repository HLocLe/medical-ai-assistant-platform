using System.Text.Json.Serialization;

namespace MedMateAI.Application.DTOs.SymptomAnalysis.Responses;

public sealed class SymptomAnalysisAiDepartment
{
    [JsonPropertyName("departmentName")]
    public string? DepartmentName { get; set; }

    [JsonPropertyName("confidenceScore")]
    public double? ConfidenceScore { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("priorityRank")]
    public int PriorityRank { get; set; }

    [JsonPropertyName("isEmergencySuggested")]
    public bool IsEmergencySuggested { get; set; }
}
