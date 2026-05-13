using MedMateAI.Domain.Enums;

namespace MedMateAI.Application.DTOs.Users.Requests;

public sealed class UpdateUserRequest
{
    public string? DisplayName { get; set; }

    public string? Address { get; set; }

    public Gender? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }
}
