namespace MedMateAI.Application.DTOs.UserSubscriptions.Requests;

public sealed class CheckoutSubscriptionRequest
{
    public Guid PlanId { get; set; }

    public bool AutoRenew { get; set; }
}
