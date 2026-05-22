using MedMateAI.Domain.Common;
using MedMateAI.Domain.Entities;

namespace MedMateAI.Domain.Repository;

public interface IDoctorRepository : IGenericRepository<Doctor>
{
    Task<PagedResult<Doctor>> GetPagedWithDetailsAsync(
        int pageNumber,
        int pageSize,
        string? search = null,
        Guid? facilityId = null,
        Guid? departmentId = null,
        bool? isActive = null,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Doctor>> GetActiveWithDetailsAsync(
        Guid? facilityId = null,
        Guid? departmentId = null,
        string? search = null,
        CancellationToken cancellationToken = default);

    Task<Doctor?> GetByIdWithDetailsAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
