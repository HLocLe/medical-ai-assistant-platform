namespace MedMateAI.Application.DTOs.AIConfigs.Responses;

public sealed class AIConfigResponse
{
    public Guid Id { get; set; }

    public string TaskType { get; set; } = string.Empty;

    public string? SystemPrompt { get; set; }

    public string? ModelParams { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
