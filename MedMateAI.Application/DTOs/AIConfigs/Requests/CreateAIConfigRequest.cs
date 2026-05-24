namespace MedMateAI.Application.DTOs.AIConfigs.Requests;

public sealed class CreateAIConfigRequest
{
    public string TaskType { get; set; } = string.Empty;

    public string? SystemPrompt { get; set; }

    public string? Model { get; set; }

    public decimal? Temperature { get; set; }

    public int? MaxTokens { get; set; }

    public bool IsActive { get; set; } = true;
}
