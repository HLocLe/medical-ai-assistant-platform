using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MedMateAI.Application.DTOs.SymptomAnalysis.Responses.MedGemma;
using MedMateAI.Application.IService;
using MedMateAI.Infrastructure.AI.DTOs.MedGemma;
using MedMateAI.Infrastructure.AI.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MedMateAI.Infrastructure.AI;

public sealed class MedGemmaChatService : IMedGemmaChatService
{
    private static readonly JsonSerializerOptions ResponseJsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private readonly HttpClient _httpClient;
    private readonly MedGemmaOptions _options;
    private readonly ILogger<MedGemmaChatService> _logger;

    public MedGemmaChatService(
        HttpClient httpClient,
        IOptions<MedGemmaOptions> options,
        ILogger<MedGemmaChatService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<MedGemmaChatResult> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            throw new ArgumentException("Prompt is required.");
        }

        if (string.IsNullOrWhiteSpace(_options.BaseUrl))
        {
            throw new InvalidOperationException("MedGemma:BaseUrl is not configured.");
        }

        if (string.IsNullOrWhiteSpace(_options.Model))
        {
            throw new InvalidOperationException("MedGemma:Model is not configured.");
        }

        var apiKey = string.IsNullOrWhiteSpace(_options.ApiKey) ? "EMPTY" : _options.ApiKey.Trim();
        var baseUrl = _options.BaseUrl.Trim().TrimEnd('/');
        var model = _options.Model.Trim();

        var payload = new
        {
            model,
            messages = new object[]
            {
                new { role = "user", content = prompt.Trim() },
            },
            temperature = _options.Temperature,
            max_tokens = _options.MaxTokens,
        };

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/chat/completions");
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        httpRequest.Content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json");

        using var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        if (!httpResponse.IsSuccessStatusCode)
        {
            _logger.LogWarning(
                "MedGemma request failed with status code {StatusCode}. Response: {ResponseBody}",
                (int)httpResponse.StatusCode,
                Truncate(responseBody, 500));

            throw new InvalidOperationException(
                $"MedGemma request failed with status code {(int)httpResponse.StatusCode}. Response: {Truncate(responseBody, 500)}");
        }

        MedGemmaChatCompletionResponse? response;
        try
        {
            response = JsonSerializer.Deserialize<MedGemmaChatCompletionResponse>(responseBody, ResponseJsonOptions);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Failed to parse MedGemma response.", ex);
        }

        var content = response?.Choices?.FirstOrDefault()?.Message?.Content;

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new InvalidOperationException("MedGemma response does not contain message content.");
        }

        return new MedGemmaChatResult
        {
            Content = content.Trim(),
            Model = response?.Model ?? model,
        };
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
