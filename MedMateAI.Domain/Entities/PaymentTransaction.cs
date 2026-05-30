namespace MedMateAI.Domain.Entities;

public sealed class PaymentTransaction : BaseEntity
{
    public Guid? PaymentId { get; set; }

    public Guid UserId { get; set; }

    public Guid UserSubscriptionId { get; set; }

    public decimal Amount { get; set; }

    public string? PaymentProvider { get; set; }

    public string? Status { get; set; }

    public DateTime? PaidAt { get; set; }

    public string? TransactionReference { get; set; }

    public string? ProviderTransactionId { get; set; }

    public string? ProviderResponseCode { get; set; }

    public string? ProviderTransactionStatus { get; set; }

    public string? BankCode { get; set; }

    public string? CardType { get; set; }

    public string? OrderInfo { get; set; }

    public string? RawResponse { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public Payment? Payment { get; set; }

    public UserSubscription UserSubscription { get; set; } = null!;
}
