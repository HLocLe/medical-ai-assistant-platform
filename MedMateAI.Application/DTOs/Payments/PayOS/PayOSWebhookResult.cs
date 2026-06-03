namespace MedMateAI.Application.DTOs.Payments.PayOS;

public sealed class PayOSWebhookResult
{
    public bool IsValid { get; set; }

    public bool IsPaid { get; set; }

    public bool IsCancelled { get; set; }

    public long OrderCode { get; set; }

    public int Amount { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public string? Reference { get; set; }

    public string? PaymentLinkId { get; set; }

    public string? Currency { get; set; }

    public string? RawBody { get; set; }
}
