using System.Text.Json.Serialization;

namespace MedMateAI.Infrastructure.AI.DTOs.OpenRouter;

internal sealed class OpenRouterChoice
{
    [JsonPropertyName("message")]
    public OpenRouterMessage? Message { get; set; }
}
