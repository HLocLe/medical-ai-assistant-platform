using MedMateAI.Domain.Entities;

namespace MedMateAI.Domain.Repository;

public interface IMedicalDepartmentRepository : IGenericRepository<MedicalDepartment>
{
    Task<IReadOnlyList<MedicalDepartment>> GetActiveAsync(
        CancellationToken cancellationToken = default);

    Task<MedicalDepartment?> GetActiveByChapterCodeAsync(
        string chapterCode,
        CancellationToken cancellationToken = default);
}
