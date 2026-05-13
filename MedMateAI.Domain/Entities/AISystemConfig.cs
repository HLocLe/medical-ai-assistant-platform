namespace MedMateAI.Domain.Entities;

public sealed class AISystemConfig : BaseEntity
{
    public Guid? SymptomAnalysisSessionId { get; set; }

    public Guid? MedicationScanId { get; set; }

    public Guid? VisitId { get; set; }

    public Guid? RecoveryPlanId { get; set; }

    public Guid? DrugAnalysisId { get; set; }

    public Guid? ConsultationSessionId { get; set; }

    public string TaskType { get; set; } = string.Empty;

    public string? SystemPrompt { get; set; }

    public string? ModelParams { get; set; }

    public bool IsActive { get; set; }

    public SymptomAnalysisSession? SymptomAnalysisSession { get; set; }

    public MedicationScan? MedicationScan { get; set; }

    public MedicalVisit? Visit { get; set; }

    public RecoveryPlan? RecoveryPlan { get; set; }

    public DrugAnalysis? DrugAnalysis { get; set; }

    public ConsultationSession? ConsultationSession { get; set; }
}
