using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Repository;

namespace MedMateAI.Infrastructure.Repositories;

public sealed class DepartmentRecommendationRepository
    : GenericRepository<DepartmentRecommendation>, IDepartmentRecommendationRepository
{
    public DepartmentRecommendationRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
