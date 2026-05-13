namespace MedMateAI.Domain.Entities;

public sealed class FeedbackReview : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid VisitId { get; set; }

    public Guid FacilityId { get; set; }

    public Guid DoctorId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public string? Status { get; set; }

    public MedicalVisit Visit { get; set; } = null!;

    public MedicalFacility Facility { get; set; } = null!;

    public Doctor Doctor { get; set; } = null!;
}
