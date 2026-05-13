namespace MedMateAI.Domain.Entities;

public sealed class MedicationScanResult : BaseEntity
{
    public Guid MedicationScanId { get; set; }

    public Guid MedicineId { get; set; }

    public double? ConfidenceScore { get; set; }

    public string? DetectedName { get; set; }

    public string? DetectedDosage { get; set; }

    public MedicationScan MedicationScan { get; set; } = null!;

    public Medicine Medicine { get; set; } = null!;
}
