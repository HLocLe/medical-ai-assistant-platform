namespace MedMateAI.Domain.Entities;

public sealed class SessionClinicalQuestionAnswer : BaseEntity
{
    public Guid SymptomAnalysisSessionId { get; set; }

    public Guid ClinicalQuestionId { get; set; }

    public bool Answer { get; set; }

    public SymptomAnalysisSession SymptomAnalysisSession { get; set; } = null!;

    public ClinicalQuestion ClinicalQuestion { get; set; } = null!;
}
