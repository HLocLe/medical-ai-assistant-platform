using System.Text.Json.Serialization;

namespace MedMateAI.Infrastructure.AI.DTOs.OpenRouter;

internal sealed class OpenRouterResponse
{
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("choices")]
    public List<OpenRouterChoice> Choices { get; set; } = [];
}
