namespace MedMateAI.Domain.Entities;

public sealed class MedicalVisit : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid FacilityId { get; set; }

    public Guid DepartmentId { get; set; }

    public Guid DoctorId { get; set; }

    public DateTime? VisitDate { get; set; }

    public string? Reason { get; set; }

    public string? DiagnosisNote { get; set; }

    public string? Status { get; set; }

    public MedicalFacility Facility { get; set; } = null!;

    public MedicalDepartment Department { get; set; } = null!;

    public Doctor Doctor { get; set; } = null!;

    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public ICollection<FeedbackReview> FeedbackReviews { get; set; } = new List<FeedbackReview>();

    public ICollection<TreatmentJourney> TreatmentJourneys { get; set; } = new List<TreatmentJourney>();

    public ICollection<AISystemConfig> AISystemConfigs { get; set; } = new List<AISystemConfig>();
}
