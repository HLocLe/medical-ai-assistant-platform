namespace MedMateAI.Domain.Entities;

public sealed class MedicationScan : BaseEntity
{
    public Guid UserId { get; set; }

    public string? ImageUrl { get; set; }

    public string? ExtractedText { get; set; }

    public string? Status { get; set; }

    public ICollection<MedicationScanResult> MedicationScanResults { get; set; } = new List<MedicationScanResult>();

    public ICollection<AISystemConfig> AISystemConfigs { get; set; } = new List<AISystemConfig>();
}
