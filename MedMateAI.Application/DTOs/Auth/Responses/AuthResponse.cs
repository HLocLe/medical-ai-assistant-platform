namespace MedMateAI.Application.DTOs.Auth.Responses;

public class AuthResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid UserId { get; set; }

    public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();
    
    public DateTimeOffset ExpiresAtUtc { get; set; }

    public bool FirstLogin { get; set; }

    public bool IsProfileCompleted { get; set; }
}
