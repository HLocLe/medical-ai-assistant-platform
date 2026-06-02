using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace MedMateAI.Infrastructure.Repositories;

public sealed class PaymentRepository
    : GenericRepository<Payment>, IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<Payment?> GetByIdWithSubscriptionAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        return await _context.Payments
            .Include(x => x.UserSubscription)
            .ThenInclude(x => x.Plan)
            .Include(x => x.Transactions)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
