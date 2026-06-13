using System.Text.Json.Serialization;

namespace MedMateAI.Infrastructure.AI.DTOs.MedGemma;

internal sealed class MedGemmaChoice
{
    [JsonPropertyName("message")]
    public MedGemmaMessage? Message { get; set; }
}
