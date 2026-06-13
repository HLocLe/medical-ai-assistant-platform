using System.Text.Json.Serialization;

namespace MedMateAI.Infrastructure.Translation.DTOs;

public sealed class AzureTranslateRequest
{
    [JsonPropertyName("Text")]
    public string Text { get; set; } = string.Empty;
}
