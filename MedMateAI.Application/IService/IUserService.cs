using MedMateAI.Application.DTOs.Request;
using MedMateAI.Application.DTOs.Response;
using MedMateAI.Domain.Entities;

namespace MedMateAI.Application.IService;

public interface IUserService
{
    Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default);

    Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> UserExistsAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> IsInRoleAsync(Guid userId, string role, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<string>> GetRolesAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>Lists non-deleted users, 10 per page. Page numbers are 1-based.</summary>
    Task<PagedUsersResponse> ListUsersAsync(int pageNumber, CancellationToken cancellationToken = default);

    Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(
        Guid userId,
        UpdateUserRequest request,
        CancellationToken cancellationToken = default);

    Task<(bool Succeeded, IEnumerable<string> Errors)> SoftDeleteUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<(bool Succeeded, IEnumerable<string> Errors)> ApproveUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}
