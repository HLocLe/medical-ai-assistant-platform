using System.Text.Json;
using MedMateAI.Application.DTOs.Payments.PayOS;
using MedMateAI.Application.IService;
using Microsoft.Extensions.Options;
using PayOS;
using PayOS.Models.V2.PaymentRequests;
using PayOS.Models.Webhooks;

namespace MedMateAI.Infrastructure.Payments.PayOS;

public sealed class PayOSService : IPayOSService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private readonly PayOSOptions _options;
    private readonly PayOSClient _payOsClient;

    public PayOSService(IOptions<PayOSOptions> options)
    {
        _options = options.Value;
        ValidateOptions(_options);
        _payOsClient = new PayOSClient(_options.ClientId, _options.ApiKey, _options.ChecksumKey);
    }

    public async Task<PayOSCreatePaymentResult> CreatePaymentLinkAsync(
        PayOSCreatePaymentRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new ArgumentException("Request is required.");
        }

        if (request.OrderCode <= 0)
        {
            throw new ArgumentException("OrderCode must be greater than 0.");
        }

        if (request.Amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0.");
        }

        var description = NormalizeDescription(request.Description);
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description is required.");
        }

        var paymentRequest = new CreatePaymentLinkRequest
        {
            OrderCode = request.OrderCode,
            Amount = request.Amount,
            Description = description,
            ReturnUrl = string.IsNullOrWhiteSpace(request.ReturnUrl) ? _options.ReturnUrl : request.ReturnUrl,
            CancelUrl = string.IsNullOrWhiteSpace(request.CancelUrl) ? _options.CancelUrl : request.CancelUrl,
        };

        _ = cancellationToken;
        var response = await _payOsClient.PaymentRequests.CreateAsync(paymentRequest);

        return new PayOSCreatePaymentResult
        {
            CheckoutUrl = response.CheckoutUrl,
            PaymentLinkId = response.PaymentLinkId,
            OrderCode = response.OrderCode,
            Status = response.Status.ToString().ToUpperInvariant(),
            RawResponse = JsonSerializer.Serialize(response),
        };
    }

    public async Task<PayOSWebhookResult> VerifyWebhookAsync(
        string rawBody,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(rawBody))
        {
            return new PayOSWebhookResult
            {
                IsValid = false,
                RawBody = rawBody,
            };
        }

        Webhook? webhook;
        try
        {
            webhook = JsonSerializer.Deserialize<Webhook>(rawBody, JsonOptions);
        }
        catch (JsonException)
        {
            return new PayOSWebhookResult
            {
                IsValid = false,
                RawBody = rawBody,
            };
        }

        if (webhook is null)
        {
            return new PayOSWebhookResult
            {
                IsValid = false,
                RawBody = rawBody,
            };
        }

        try
        {
            _ = cancellationToken;
            var verifiedData = await _payOsClient.Webhooks.VerifyAsync(webhook);
            var (isCancelled, status, cancelFlag) = ParseCancelInfo(rawBody);

            var isPaid = webhook.Success
                && (string.Equals(webhook.Code, "00", StringComparison.Ordinal)
                    || string.Equals(verifiedData.Code, "00", StringComparison.Ordinal))
                && !string.Equals(status, "CANCELLED", StringComparison.OrdinalIgnoreCase)
                && cancelFlag != true;

            if (!isCancelled && (string.Equals(status, "CANCELLED", StringComparison.OrdinalIgnoreCase) || cancelFlag == true))
            {
                isCancelled = true;
            }

            return new PayOSWebhookResult
            {
                IsValid = true,
                IsPaid = isPaid,
                IsCancelled = isCancelled,
                OrderCode = verifiedData.OrderCode,
                Amount = Convert.ToInt32(verifiedData.Amount),
                Code = verifiedData.Code,
                Description = verifiedData.Description2,
                Reference = verifiedData.Reference,
                PaymentLinkId = verifiedData.PaymentLinkId,
                Currency = verifiedData.Currency,
                RawBody = rawBody,
            };
        }
        catch
        {
            return new PayOSWebhookResult
            {
                IsValid = false,
                RawBody = rawBody,
            };
        }
    }

    private static void ValidateOptions(PayOSOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.ClientId))
        {
            throw new InvalidOperationException("PayOS:ClientId is required.");
        }

        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            throw new InvalidOperationException("PayOS:ApiKey is required.");
        }

        if (string.IsNullOrWhiteSpace(options.ChecksumKey))
        {
            throw new InvalidOperationException("PayOS:ChecksumKey is required.");
        }

        if (string.IsNullOrWhiteSpace(options.ReturnUrl))
        {
            throw new InvalidOperationException("PayOS:ReturnUrl is required.");
        }

        if (string.IsNullOrWhiteSpace(options.CancelUrl))
        {
            throw new InvalidOperationException("PayOS:CancelUrl is required.");
        }
    }

    private static string NormalizeDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return string.Empty;
        }

        var normalized = description.Trim();
        if (normalized.Length <= 25)
        {
            return normalized;
        }

        return normalized[..25];
    }

    private static (bool IsCancelled, string? Status, bool? CancelFlag) ParseCancelInfo(string rawBody)
    {
        try
        {
            using var document = JsonDocument.Parse(rawBody);
            var root = document.RootElement;
            var data = root.TryGetProperty("data", out var dataElement)
                ? dataElement
                : root;

            string? status = null;
            if (data.ValueKind == JsonValueKind.Object && data.TryGetProperty("status", out var statusElement))
            {
                status = statusElement.GetString();
            }

            bool? cancel = null;
            if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("cancel", out var cancelElement))
            {
                cancel = cancelElement.ValueKind switch
                {
                    JsonValueKind.True => true,
                    JsonValueKind.False => false,
                    JsonValueKind.String when bool.TryParse(cancelElement.GetString(), out var parsed) => parsed,
                    _ => null,
                };
            }

            var isCancelled =
                string.Equals(status, "CANCELLED", StringComparison.OrdinalIgnoreCase)
                || cancel == true;

            return (isCancelled, status, cancel);
        }
        catch
        {
            return (false, null, null);
        }
    }
}
