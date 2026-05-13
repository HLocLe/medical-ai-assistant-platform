namespace MedMateAI.Application.DTOs.Auth.Requests;

public sealed class GoogleLoginRequest
{
    public string Credential { get; set; } = string.Empty;
}
