namespace MedMateAI.Infrastructure.Translation.Options;

public sealed class AzureTranslatorOptions
{
    public const string SectionName = "AzureTranslator";

    public string SubscriptionKey { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;

    public string Endpoint { get; set; } = string.Empty;

    public string DefaultSourceLanguage { get; set; } = string.Empty;

    public string DefaultTargetLanguage { get; set; } = string.Empty;
}
