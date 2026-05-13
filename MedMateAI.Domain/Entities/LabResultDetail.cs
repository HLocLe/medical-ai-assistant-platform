namespace MedMateAI.Domain.Entities;

public sealed class LabResultDetail : BaseEntity
{
    public Guid LabResultId { get; set; }

    public string? TestName { get; set; }

    public string? Value { get; set; }

    public string? Unit { get; set; }

    public double? NormalRangeMin { get; set; }

    public double? NormalRangeMax { get; set; }

    public string? Interpretation { get; set; }

    public LabResult LabResult { get; set; } = null!;
}
