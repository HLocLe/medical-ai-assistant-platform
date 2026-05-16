namespace MedMateAI.Application.DTOs.WebChatbot.Requests;

public sealed class AIProviderChatRequest
{
    public string SystemPrompt { get; set; } = string.Empty;

    public string UserMessage { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public decimal? Temperature { get; set; }

    public int? MaxTokens { get; set; }
}
