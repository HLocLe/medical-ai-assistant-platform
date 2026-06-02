namespace MedMateAI.Application.DTOs.Payments.Responses;

public sealed class PayOSReturnResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public Guid? PaymentId { get; set; }

    public Guid? SubscriptionId { get; set; }

    public string? OrderCode { get; set; }

    public string? Status { get; set; }

    public bool Cancelled { get; set; }
}
