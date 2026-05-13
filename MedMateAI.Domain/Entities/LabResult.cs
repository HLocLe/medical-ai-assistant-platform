namespace MedMateAI.Domain.Entities;

public sealed class LabResult : BaseEntity
{
    public Guid MedicalRecordId { get; set; }

    public string? LabName { get; set; }

    public DateTime? TestedAt { get; set; }

    public string? OverallConclusion { get; set; }

    public MedicalRecord MedicalRecord { get; set; } = null!;

    public ICollection<LabResultDetail> LabResultDetails { get; set; } = new List<LabResultDetail>();
}
