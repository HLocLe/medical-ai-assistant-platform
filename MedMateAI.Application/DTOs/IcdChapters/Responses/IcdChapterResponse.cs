namespace MedMateAI.Application.DTOs.IcdChapters.Responses;

public sealed class IcdChapterResponse
{
    public Guid Id { get; set; }

    public string ChapterCode { get; set; } = string.Empty;

    public string ChapterName { get; set; } = string.Empty;

    public Dictionary<string, int> KeywordWeights { get; set; } = new();

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
