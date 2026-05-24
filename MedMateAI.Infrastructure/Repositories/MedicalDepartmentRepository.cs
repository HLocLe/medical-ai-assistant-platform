using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Repository;

namespace MedMateAI.Infrastructure.Repositories;

public sealed class MedicalDepartmentRepository
    : GenericRepository<MedicalDepartment>, IMedicalDepartmentRepository
{
    public MedicalDepartmentRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
