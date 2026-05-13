namespace MedMateAI.Domain.Entities;

public sealed class PaymentTransaction : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid UserSubscriptionId { get; set; }

    public decimal Amount { get; set; }

    public string? PaymentProvider { get; set; }

    public string? Status { get; set; }

    public DateTime? PaidAt { get; set; }

    public UserSubscription UserSubscription { get; set; } = null!;
}
