namespace MedMateAI.Application.DTOs.MedicalDepartments.Responses;

public sealed class MedicalDepartmentResponse
{
    public Guid Id { get; set; }

    public string? DepartmentName { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
