namespace MedMateAI.Infrastructure.AI.Options;

public sealed class MedGemmaOptions
{
    public const string SectionName = "MedGemma";

    public string ApiKey { get; set; } = "EMPTY";

    public string BaseUrl { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public decimal Temperature { get; set; } = 0.1m;

    public int MaxTokens { get; set; } = 2048;
}
