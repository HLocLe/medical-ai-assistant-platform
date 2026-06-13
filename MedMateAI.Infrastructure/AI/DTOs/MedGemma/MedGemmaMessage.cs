using System.Text.Json.Serialization;

namespace MedMateAI.Infrastructure.AI.DTOs.MedGemma;

internal sealed class MedGemmaMessage
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }
}
