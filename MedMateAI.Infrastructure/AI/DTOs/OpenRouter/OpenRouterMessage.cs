using System.Text.Json.Serialization;

namespace MedMateAI.Infrastructure.AI.DTOs.OpenRouter;

internal sealed class OpenRouterMessage
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }
}
