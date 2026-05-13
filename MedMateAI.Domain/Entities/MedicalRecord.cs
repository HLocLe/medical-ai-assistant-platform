namespace MedMateAI.Domain.Entities;

public sealed class MedicalRecord : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid VisitId { get; set; }

    public string? RecordType { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateOnly? RecordDate { get; set; }

    public string? Status { get; set; }

    public MedicalVisit Visit { get; set; } = null!;

    public ICollection<MedicalRecordFile> MedicalRecordFiles { get; set; } = new List<MedicalRecordFile>();

    public ICollection<LabResult> LabResults { get; set; } = new List<LabResult>();
}
