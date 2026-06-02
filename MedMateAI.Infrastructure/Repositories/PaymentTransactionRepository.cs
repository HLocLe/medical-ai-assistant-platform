using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace MedMateAI.Infrastructure.Repositories;

public sealed class PaymentTransactionRepository
    : GenericRepository<PaymentTransaction>, IPaymentTransactionRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentTransactionRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<PaymentTransaction?> GetByTransactionReferenceAsync(
        string transactionReference,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(transactionReference))
        {
            return null;
        }

        var normalized = transactionReference.Trim();

        return await _context.PaymentTransactions
            .Include(x => x.Payment)
            .ThenInclude(x => x!.UserSubscription)
            .ThenInclude(x => x.Plan)
            .Include(x => x.UserSubscription)
            .ThenInclude(x => x.Plan)
            .FirstOrDefaultAsync(
                x => x.TransactionReference != null && x.TransactionReference == normalized,
                cancellationToken);
    }

    public async Task<IReadOnlyList<PaymentTransaction>> GetByPaymentIdAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default)
    {
        if (paymentId == Guid.Empty)
        {
            return Array.Empty<PaymentTransaction>();
        }

        return await _context.PaymentTransactions
            .AsNoTracking()
            .Where(x => x.PaymentId == paymentId)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }
}
