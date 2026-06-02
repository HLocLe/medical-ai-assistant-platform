using MedMateAI.Domain.Entities;

namespace MedMateAI.Domain.Repository;

public interface IPaymentRepository : IGenericRepository<Payment>
{
    Task<Payment?> GetByIdWithSubscriptionAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
