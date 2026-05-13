namespace MedMateAI.Domain.Entities;

public sealed class MedicalRecordFile : BaseEntity
{
    public Guid MedicalRecordId { get; set; }

    public string? FileUrl { get; set; }

    public string? FileType { get; set; }

    public string? OriginalFileName { get; set; }

    public DateTime? UploadedAt { get; set; }

    public MedicalRecord MedicalRecord { get; set; } = null!;
}
