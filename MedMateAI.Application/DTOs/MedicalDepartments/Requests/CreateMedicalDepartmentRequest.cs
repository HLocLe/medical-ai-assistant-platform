namespace MedMateAI.Application.DTOs.MedicalDepartments.Requests;

public sealed class CreateMedicalDepartmentRequest
{
    public string? DepartmentName { get; set; }

    public string? Description { get; set; }
}
