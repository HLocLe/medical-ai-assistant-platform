namespace MedMateAI.Application.DTOs.Auth.Requests;

public sealed class ChangePasswordWithOtpRequest
{
    public string Email { get; set; } = string.Empty;

    public string Otp { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;

    public string ConfirmNewPassword { get; set; } = string.Empty;
}
