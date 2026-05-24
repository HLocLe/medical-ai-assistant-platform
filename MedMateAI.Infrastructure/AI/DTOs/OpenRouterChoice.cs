using System.Text.Json.Serialization;

namespace MedMateAI.Infrastructure.AI.DTOs;

internal sealed class OpenRouterChoice
{
    [JsonPropertyName("message")]
    public OpenRouterMessage? Message { get; set; }
}
