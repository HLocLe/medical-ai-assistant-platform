using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Repository;

namespace MedMateAI.Infrastructure.Repositories;

public sealed class SessionSymptomRepository
    : GenericRepository<SessionSymptom>, ISessionSymptomRepository
{
    public SessionSymptomRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
