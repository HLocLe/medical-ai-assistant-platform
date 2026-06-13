using MedMateAI.Domain.Common;
using MedMateAI.Domain.Entities;

namespace MedMateAI.Domain.Repository;

public interface ISymptomAnalysisSessionRepository : IGenericRepository<SymptomAnalysisSession>
{
    Task<PagedResult<SymptomAnalysisSession>> GetPagedByUserIdAsync(
        Guid userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
}
