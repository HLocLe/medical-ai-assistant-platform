namespace MedMateAI.Domain.Entities;

public sealed class DepartmentRecommendation : BaseEntity
{
    public Guid SymptomAnalysisSessionId { get; set; }

    public Guid DepartmentId { get; set; }

    public double? ConfidenceScore { get; set; }

    public string? Reason { get; set; }

    public int PriorityRank { get; set; }

    public bool IsEmergencySuggested { get; set; }

    public SymptomAnalysisSession SymptomAnalysisSession { get; set; } = null!;

    public MedicalDepartment Department { get; set; } = null!;
}
