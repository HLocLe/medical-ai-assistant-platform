using MedMateAI.Domain.Entities;

namespace MedMateAI.Domain.Repository;

public interface ISessionClinicalQuestionAnswerRepository : IGenericRepository<SessionClinicalQuestionAnswer>
{
    Task<IReadOnlyList<SessionClinicalQuestionAnswer>> GetBySessionIdAsync(
        Guid sessionId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SessionClinicalQuestionAnswer>> GetTrackedBySessionIdAsync(
        Guid sessionId,
        CancellationToken cancellationToken = default);
}
