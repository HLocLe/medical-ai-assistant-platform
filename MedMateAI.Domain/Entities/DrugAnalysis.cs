namespace MedMateAI.Domain.Entities;

public sealed class DrugAnalysis : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid TreatmentJourneyId { get; set; }

    public string? Status { get; set; }

    public TreatmentJourney TreatmentJourney { get; set; } = null!;

    public ICollection<DrugAnalysisResult> DrugAnalysisResults { get; set; } = new List<DrugAnalysisResult>();

    public ICollection<AISystemConfig> AISystemConfigs { get; set; } = new List<AISystemConfig>();
}
