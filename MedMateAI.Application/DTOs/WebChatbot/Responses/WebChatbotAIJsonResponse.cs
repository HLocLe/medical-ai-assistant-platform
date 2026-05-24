using System.Text.Json.Serialization;

namespace MedMateAI.Application.DTOs.WebChatbot.Responses;

public sealed class WebChatbotAIJsonResponse
{
    [JsonPropertyName("answer")]
    public string Answer { get; set; } = string.Empty;

    [JsonPropertyName("recommendedPlanIds")]
    public List<Guid> RecommendedPlanIds { get; set; } = [];

    [JsonPropertyName("intent")]
    public string? Intent { get; set; }

    [JsonPropertyName("needsMoreInformation")]
    public bool NeedsMoreInformation { get; set; }
}
