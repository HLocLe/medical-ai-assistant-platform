namespace MedMateAI.Application.DTOs.WebChatbot.Responses;

public sealed class AIProviderChatResult
{
    public string Content { get; set; } = string.Empty;

    public string? Model { get; set; }

    public string? Provider { get; set; }
}
