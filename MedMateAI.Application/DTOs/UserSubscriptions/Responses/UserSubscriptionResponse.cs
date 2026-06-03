using MedMateAI.Domain.Enums;

namespace MedMateAI.Application.DTOs.UserSubscriptions.Responses;

public sealed class UserSubscriptionResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid PlanId { get; set; }

    public string? PlanName { get; set; }

    public decimal Price { get; set; }

    public int DurationInDays { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public SubscriptionStatus Status { get; set; }

    public string StatusName { get; set; } = string.Empty;

    public bool AutoRenew { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
