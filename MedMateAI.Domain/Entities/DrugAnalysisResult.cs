namespace MedMateAI.Domain.Entities;

public sealed class DrugAnalysisResult : BaseEntity
{
    public Guid DrugAnalysisId { get; set; }

    public Guid MedicineId { get; set; }

    public string? CombinationVerdict { get; set; }

    public string? InteractionDetail { get; set; }

    public string? DietaryInteraction { get; set; }

    public string? UsageOptimization { get; set; }

    public string? Precaution { get; set; }

    public string? Severity { get; set; }

    public DrugAnalysis DrugAnalysis { get; set; } = null!;

    public Medicine Medicine { get; set; } = null!;
}
