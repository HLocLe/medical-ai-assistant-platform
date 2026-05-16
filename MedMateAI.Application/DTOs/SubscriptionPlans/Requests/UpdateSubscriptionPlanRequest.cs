namespace MedMateAI.Application.DTOs.SubscriptionPlans.Requests;

public sealed class UpdateSubscriptionPlanRequest
{
    public string? PlanName { get; set; }

    public decimal? Price { get; set; }

    public int? DurationInDays { get; set; }

    public string? FeatureLimitJson { get; set; }

    public bool? IsActive { get; set; }
}
