namespace MedMateAI.Application.DTOs.MedicalDepartments.Requests;

public sealed class UpdateMedicalDepartmentRequest
{
    public string? DepartmentName { get; set; }

    public string? Description { get; set; }
}
