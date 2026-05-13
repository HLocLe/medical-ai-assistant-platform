using MedMateAI.Domain.Entities;

namespace MedMateAI.Application.DTOs.Users.Responses;

public sealed class PagedUsersResponse
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public int TotalPages { get; set; }

    public IReadOnlyList<User> Items { get; set; } = Array.Empty<User>();
}
