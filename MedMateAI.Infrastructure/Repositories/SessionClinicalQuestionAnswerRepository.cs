using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace MedMateAI.Infrastructure.Repositories;

public sealed class SessionClinicalQuestionAnswerRepository
    : GenericRepository<SessionClinicalQuestionAnswer>, ISessionClinicalQuestionAnswerRepository
{
    private readonly ApplicationDbContext _context;

    public SessionClinicalQuestionAnswerRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<SessionClinicalQuestionAnswer>> GetBySessionIdAsync(
        Guid sessionId,
        CancellationToken cancellationToken = default)
    {
        return await _context.SessionClinicalQuestionAnswers
            .AsNoTracking()
            .Include(answer => answer.ClinicalQuestion)
            .Where(answer => !answer.IsDeleted && answer.SymptomAnalysisSessionId == sessionId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<SessionClinicalQuestionAnswer>> GetTrackedBySessionIdAsync(
        Guid sessionId,
        CancellationToken cancellationToken = default)
    {
        return await _context.SessionClinicalQuestionAnswers
            .Include(answer => answer.ClinicalQuestion)
            .Where(answer => !answer.IsDeleted && answer.SymptomAnalysisSessionId == sessionId)
            .ToListAsync(cancellationToken);
    }
}
