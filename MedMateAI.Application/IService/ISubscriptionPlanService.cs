using MedMateAI.Application.DTOs.SubscriptionPlans.Requests;
using MedMateAI.Application.DTOs.SubscriptionPlans.Responses;

namespace MedMateAI.Application.IService;

public interface ISubscriptionPlanService
{
    Task<IReadOnlyList<SubscriptionPlanResponse>> ListSubscriptionPlansAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SubscriptionPlanResponse>> ListActiveSubscriptionPlansAsync(
        CancellationToken cancellationToken = default);

    Task<SubscriptionPlanResponse?> GetSubscriptionPlanByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<SubscriptionPlanResponse> CreateSubscriptionPlanAsync(
        CreateSubscriptionPlanRequest request,
        CancellationToken cancellationToken = default);

    Task<SubscriptionPlanResponse?> UpdateSubscriptionPlanAsync(
        Guid id,
        UpdateSubscriptionPlanRequest request,
        CancellationToken cancellationToken = default);

    Task<SubscriptionPlanResponse?> UpdateSubscriptionPlanStatusAsync(
        Guid id,
        UpdateSubscriptionPlanStatusRequest request,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteSubscriptionPlanAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
