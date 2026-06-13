using System.Text;
using System.Text.Json;
using MedMateAI.Application.IService;
using MedMateAI.Infrastructure.Translation.DTOs;
using MedMateAI.Infrastructure.Translation.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MedMateAI.Infrastructure.Translation;

public sealed class AzureTranslationService : ITranslationService
{
    private static readonly JsonSerializerOptions RequestJsonOptions = new()
    {
        PropertyNamingPolicy = null,
    };

    private static readonly JsonSerializerOptions ResponseJsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private readonly HttpClient _httpClient;
    private readonly AzureTranslatorOptions _options;
    private readonly ILogger<AzureTranslationService> _logger;

    public AzureTranslationService(
        HttpClient httpClient,
        IOptions<AzureTranslatorOptions> options,
        ILogger<AzureTranslationService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<string> TranslateToEnglishAsync(
        string text,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        var requestUri = BuildTranslateUri(_options.DefaultSourceLanguage, _options.DefaultTargetLanguage);

        var payload = JsonSerializer.Serialize(
            new[] { new AzureTranslateRequest { Text = text.Trim() } },
            RequestJsonOptions);

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri);
        httpRequest.Headers.Add("Ocp-Apim-Subscription-Key", _options.SubscriptionKey);
        httpRequest.Headers.Add("Ocp-Apim-Subscription-Region", _options.Region);
        httpRequest.Content = new StringContent(payload, Encoding.UTF8, "application/json");

        try
        {
            using var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

            if (!httpResponse.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "Azure Translator request failed with status {StatusCode}. Response: {ResponseBody}",
                    (int)httpResponse.StatusCode,
                    Truncate(responseBody, 500));

                return text.Trim();
            }

            var results = JsonSerializer.Deserialize<AzureTranslateResponseItem[]>(responseBody, ResponseJsonOptions);

            var translated = results?
                .FirstOrDefault()?
                .Translations?
                .FirstOrDefault()?
                .Text;

            if (string.IsNullOrWhiteSpace(translated))
            {
                _logger.LogWarning("Azure Translator returned an empty translation. Using original text.");
                return text.Trim();
            }

            return translated.Trim();
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogWarning(ex, "Azure Translator request failed. Using original text.");
            return text.Trim();
        }
    }

    private string BuildTranslateUri(string sourceLanguage, string targetLanguage)
    {
        var endpoint = _options.Endpoint.Trim();
        if (!endpoint.EndsWith('/'))
        {
            endpoint += "/";
        }

        return $"{endpoint}translate?api-version=3.0&from={Uri.EscapeDataString(sourceLanguage)}&to={Uri.EscapeDataString(targetLanguage)}";
    }

    private static string Truncate(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
        {
            return value;
        }

        return value[..maxLength];
    }
}
