using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace MedMateAI.Infrastructure.Repositories;

public sealed class IcdChapterRepository
    : GenericRepository<IcdChapter>, IIcdChapterRepository
{
    private readonly ApplicationDbContext _context;

    public IcdChapterRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<IcdChapter>> GetActiveChaptersAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.IcdChapters
            .AsNoTracking()
            .Where(chapter => !chapter.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}
