namespace MedMateAI.Application.DTOs.SymptomAnalysis.Responses;

public sealed class RecommendedDepartmentResponse
{
    public Guid DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public double? ConfidenceScore { get; set; }

    public string? Reason { get; set; }

    public int PriorityRank { get; set; }

    public bool IsEmergencySuggested { get; set; }
}
