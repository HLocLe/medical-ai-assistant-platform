namespace MedMateAI.Application.DTOs.AIConfigs.Requests;

public sealed class UpdateAIConfigRequest
{
    public string? TaskType { get; set; }

    public string? SystemPrompt { get; set; }

    public string? ModelParams { get; set; }

    public bool? IsActive { get; set; }
}
