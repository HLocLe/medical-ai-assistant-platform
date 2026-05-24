using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Repository;

namespace MedMateAI.Infrastructure.Repositories;

public sealed class SymptomAnalysisSessionRepository
    : GenericRepository<SymptomAnalysisSession>, ISymptomAnalysisSessionRepository
{
    public SymptomAnalysisSessionRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
