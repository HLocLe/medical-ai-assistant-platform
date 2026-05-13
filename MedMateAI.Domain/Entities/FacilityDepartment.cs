namespace MedMateAI.Domain.Entities;

public sealed class FacilityDepartment : BaseEntity
{
    public Guid FacilityId { get; set; }

    public Guid DepartmentId { get; set; }

    public MedicalFacility Facility { get; set; } = null!;

    public MedicalDepartment Department { get; set; } = null!;

    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
