namespace MedMateAI.Domain.Entities;

public sealed class AIAnalysis : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid TreatmentJourneyId { get; set; }

    public string? SourceType { get; set; }

    public Guid SourceId { get; set; }

    public string? Prompt { get; set; }

    public string? ResultSummary { get; set; }

    public string? DisclaimerText { get; set; }

    public string? ModelName { get; set; }

    public string? Status { get; set; }

    public TreatmentJourney TreatmentJourney { get; set; } = null!;
}
