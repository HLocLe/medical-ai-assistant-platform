namespace MedMateAI.Application.DTOs.Payments.PayOS;

public sealed class PayOSCreatePaymentResult
{
    public string CheckoutUrl { get; set; } = string.Empty;

    public string? PaymentLinkId { get; set; }

    public long OrderCode { get; set; }

    public string? Status { get; set; }

    public string? RawResponse { get; set; }
}
