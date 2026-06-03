using System.Globalization;
using MedMateAI.Application.DTOs.Payments.PayOS;
using MedMateAI.Application.DTOs.Payments.Responses;
using MedMateAI.Application.IService;
using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Enums;
using MedMateAI.Domain.Persistence;

namespace MedMateAI.Application.Service;

public sealed class PaymentService : IPaymentService
{
    private const string PaidStatus = "PAID";
    private const string PendingStatus = "PENDING";
    private const string ProcessingStatus = "PROCESSING";
    private const string CancelledStatus = "CANCELLED";

    private readonly IUnitOfWork _unitOfWork;
    private readonly IPayOSService _payOsService;

    public PaymentService(
        IUnitOfWork unitOfWork,
        IPayOSService payOsService)
    {
        _unitOfWork = unitOfWork;
        _payOsService = payOsService;
    }

    public async Task<PayOSReturnResponse> ProcessPayOSReturnAsync(
        IReadOnlyDictionary<string, string> queryParameters,
        CancellationToken cancellationToken = default)
    {
        if (!TryGetOrderCode(queryParameters, out var orderCode))
        {
            return new PayOSReturnResponse
            {
                Success = false,
                Message = "Invalid orderCode.",
            };
        }

        var transaction = await _unitOfWork.PaymentTransactions.GetByTransactionReferenceAsync(
            orderCode.ToString(CultureInfo.InvariantCulture),
            cancellationToken);

        if (transaction is null || transaction.Payment is null)
        {
            return new PayOSReturnResponse
            {
                Success = false,
                Message = "Payment transaction not found.",
                OrderCode = orderCode.ToString(CultureInfo.InvariantCulture),
            };
        }

        var status = GetQueryValue(queryParameters, "status")?.ToUpperInvariant();
        var responseCode = GetQueryValue(queryParameters, "code");
        var paymentLinkId = GetQueryValue(queryParameters, "id");
        var cancel = ParseBoolean(GetQueryValue(queryParameters, "cancel"));

        if (string.Equals(status, PaidStatus, StringComparison.Ordinal))
        {
            await MarkPaymentPaidAsync(
                transaction,
                callback: null,
                providerStatus: PaidStatus,
                responseCode: responseCode,
                providerTransactionId: paymentLinkId,
                orderInfo: null,
                rawResponse: BuildRawResponse(queryParameters),
                cancellationToken);

            return BuildReturnResponse(
                transaction,
                success: true,
                message: "Payment confirmed.",
                status: PaidStatus,
                cancelled: false,
                orderCode: orderCode);
        }

        if (cancel == true || string.Equals(status, CancelledStatus, StringComparison.Ordinal))
        {
            await MarkPaymentCancelledAsync(
                transaction,
                callback: null,
                providerStatus: CancelledStatus,
                responseCode: responseCode,
                providerTransactionId: paymentLinkId,
                rawResponse: BuildRawResponse(queryParameters),
                cancellationToken);

            return BuildReturnResponse(
                transaction,
                success: false,
                message: "Payment cancelled.",
                status: CancelledStatus,
                cancelled: true,
                orderCode: orderCode);
        }

        if (string.Equals(status, PendingStatus, StringComparison.Ordinal)
            || string.Equals(status, ProcessingStatus, StringComparison.Ordinal))
        {
            return BuildReturnResponse(
                transaction,
                success: false,
                message: "Payment is processing.",
                status: status,
                cancelled: false,
                orderCode: orderCode);
        }

        return BuildReturnResponse(
            transaction,
            success: false,
            message: "Payment not completed.",
            status: status,
            cancelled: cancel == true,
            orderCode: orderCode);
    }

    public async Task<PayOSReturnResponse> ProcessPayOSCancelAsync(
        IReadOnlyDictionary<string, string> queryParameters,
        CancellationToken cancellationToken = default)
    {
        if (!TryGetOrderCode(queryParameters, out var orderCode))
        {
            return new PayOSReturnResponse
            {
                Success = false,
                Message = "Invalid orderCode.",
            };
        }

        var transaction = await _unitOfWork.PaymentTransactions.GetByTransactionReferenceAsync(
            orderCode.ToString(CultureInfo.InvariantCulture),
            cancellationToken);

        if (transaction is null || transaction.Payment is null)
        {
            return new PayOSReturnResponse
            {
                Success = false,
                Message = "Payment transaction not found.",
                OrderCode = orderCode.ToString(CultureInfo.InvariantCulture),
            };
        }

        await MarkPaymentCancelledAsync(
            transaction,
            callback: null,
            providerStatus: CancelledStatus,
            responseCode: GetQueryValue(queryParameters, "code"),
            providerTransactionId: GetQueryValue(queryParameters, "id"),
            rawResponse: BuildRawResponse(queryParameters),
            cancellationToken);

        return BuildReturnResponse(
            transaction,
            success: false,
            message: "Payment cancelled.",
            status: CancelledStatus,
            cancelled: true,
            orderCode: orderCode);
    }

    public async Task<bool> ProcessPayOSWebhookAsync(
        string rawBody,
        CancellationToken cancellationToken = default)
    {
        var callback = await _payOsService.VerifyWebhookAsync(rawBody, cancellationToken);
        if (!callback.IsValid)
        {
            return false;
        }

        var transaction = await _unitOfWork.PaymentTransactions.GetByTransactionReferenceAsync(
            callback.OrderCode.ToString(CultureInfo.InvariantCulture),
            cancellationToken);

        if (transaction is null || transaction.Payment is null)
        {
            return false;
        }

        var amountMatches = transaction.Payment.Amount == callback.Amount;
        if (!amountMatches)
        {
            return false;
        }

        if (callback.IsPaid)
        {
            await MarkPaymentPaidAsync(
                transaction,
                callback,
                providerStatus: PaidStatus,
                responseCode: callback.Code,
                providerTransactionId: callback.Reference ?? callback.PaymentLinkId,
                orderInfo: callback.Description,
                rawResponse: callback.RawBody,
                cancellationToken);
            return true;
        }

        if (callback.IsCancelled)
        {
            await MarkPaymentCancelledAsync(
                transaction,
                callback,
                providerStatus: CancelledStatus,
                responseCode: callback.Code,
                providerTransactionId: callback.Reference ?? callback.PaymentLinkId,
                rawResponse: callback.RawBody,
                cancellationToken);
            return true;
        }

        return true;
    }

    public async Task<PaymentResponse?> GetPaymentByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        var payment = await _unitOfWork.Payments.GetByIdWithSubscriptionAsync(id, cancellationToken);
        if (payment is null || payment.IsDeleted)
        {
            return null;
        }

        return new PaymentResponse
        {
            Id = payment.Id,
            UserId = payment.UserId,
            UserSubscriptionId = payment.UserSubscriptionId,
            Amount = payment.Amount,
            Currency = payment.Currency,
            Status = payment.Status,
            StatusName = payment.Status.ToString(),
            PaidAt = payment.PaidAt,
            CreatedAt = payment.CreatedAt,
            UpdatedAt = payment.UpdatedAt,
        };
    }

    private async Task MarkPaymentPaidAsync(
        PaymentTransaction transaction,
        PayOSWebhookResult? callback,
        string providerStatus,
        string? responseCode,
        string? providerTransactionId,
        string? orderInfo,
        string? rawResponse,
        CancellationToken cancellationToken)
    {
        var payment = transaction.Payment;
        var subscription = payment?.UserSubscription;
        if (payment is null || subscription is null)
        {
            return;
        }

        if (payment.Status == PaymentStatus.Paid)
        {
            return;
        }

        var utcNow = DateTime.UtcNow;
        payment.Status = PaymentStatus.Paid;
        payment.PaidAt = utcNow;
        payment.UpdatedAt = utcNow;

        transaction.Status = "Paid";
        transaction.PaidAt = utcNow;
        transaction.ProcessedAt = utcNow;
        transaction.ProviderTransactionId = providerTransactionId ?? transaction.ProviderTransactionId;
        transaction.ProviderResponseCode = responseCode ?? transaction.ProviderResponseCode;
        transaction.ProviderTransactionStatus = providerStatus;
        transaction.OrderInfo = orderInfo ?? transaction.OrderInfo;
        transaction.RawResponse = rawResponse ?? transaction.RawResponse;
        transaction.UpdatedAt = utcNow;

        var durationInDays = subscription.Plan?.DurationInDays ?? 0;
        if (durationInDays <= 0)
        {
            durationInDays = 1;
        }

        subscription.Status = SubscriptionStatus.Active;
        subscription.StartDate = utcNow;
        subscription.EndDate = utcNow.AddDays(durationInDays);
        subscription.UpdatedAt = utcNow;

        _unitOfWork.Payments.Update(payment);
        _unitOfWork.PaymentTransactions.Update(transaction);
        _unitOfWork.UserSubscriptions.Update(subscription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _ = callback;
    }

    private async Task MarkPaymentCancelledAsync(
        PaymentTransaction transaction,
        PayOSWebhookResult? callback,
        string providerStatus,
        string? responseCode,
        string? providerTransactionId,
        string? rawResponse,
        CancellationToken cancellationToken)
    {
        var payment = transaction.Payment;
        var subscription = payment?.UserSubscription;
        if (payment is null || subscription is null)
        {
            return;
        }

        if (payment.Status == PaymentStatus.Paid)
        {
            return;
        }

        var utcNow = DateTime.UtcNow;
        payment.Status = PaymentStatus.Cancelled;
        payment.UpdatedAt = utcNow;

        transaction.Status = "Cancelled";
        transaction.ProcessedAt = utcNow;
        transaction.ProviderTransactionId = providerTransactionId ?? transaction.ProviderTransactionId;
        transaction.ProviderResponseCode = responseCode ?? transaction.ProviderResponseCode;
        transaction.ProviderTransactionStatus = providerStatus;
        transaction.RawResponse = rawResponse ?? transaction.RawResponse;
        transaction.UpdatedAt = utcNow;

        subscription.Status = SubscriptionStatus.Cancelled;
        subscription.UpdatedAt = utcNow;

        _unitOfWork.Payments.Update(payment);
        _unitOfWork.PaymentTransactions.Update(transaction);
        _unitOfWork.UserSubscriptions.Update(subscription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _ = callback;
    }

    private static bool TryGetOrderCode(
        IReadOnlyDictionary<string, string> queryParameters,
        out long orderCode)
    {
        orderCode = 0;
        var raw = GetQueryValue(queryParameters, "orderCode");
        if (string.IsNullOrWhiteSpace(raw))
        {
            return false;
        }

        return long.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out orderCode);
    }

    private static string? GetQueryValue(IReadOnlyDictionary<string, string> queryParameters, string key)
    {
        return queryParameters.TryGetValue(key, out var value) ? value : null;
    }

    private static bool? ParseBoolean(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (bool.TryParse(value, out var parsed))
        {
            return parsed;
        }

        if (string.Equals(value, "1", StringComparison.Ordinal))
        {
            return true;
        }

        if (string.Equals(value, "0", StringComparison.Ordinal))
        {
            return false;
        }

        return null;
    }

    private static string BuildRawResponse(IReadOnlyDictionary<string, string> queryParameters)
    {
        return string.Join("&", queryParameters.Select(x => $"{x.Key}={x.Value}"));
    }

    private static PayOSReturnResponse BuildReturnResponse(
        PaymentTransaction transaction,
        bool success,
        string message,
        string? status,
        bool cancelled,
        long orderCode)
    {
        return new PayOSReturnResponse
        {
            Success = success,
            Message = message,
            PaymentId = transaction.PaymentId,
            SubscriptionId = transaction.UserSubscriptionId,
            OrderCode = orderCode.ToString(CultureInfo.InvariantCulture),
            Status = status,
            Cancelled = cancelled,
        };
    }
}
