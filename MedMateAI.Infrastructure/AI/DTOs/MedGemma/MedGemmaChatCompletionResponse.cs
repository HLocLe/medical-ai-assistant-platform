using System.Text.Json.Serialization;

namespace MedMateAI.Infrastructure.AI.DTOs.MedGemma;

internal sealed class MedGemmaChatCompletionResponse
{
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("choices")]
    public List<MedGemmaChoice> Choices { get; set; } = [];
}
