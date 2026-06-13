namespace MedMateAI.Application.DTOs.IcdChapters.Requests;

public sealed class CreateIcdChapterRequest
{
    public string ChapterCode { get; set; } = string.Empty;

    public string ChapterName { get; set; } = string.Empty;

    public Dictionary<string, int> KeywordWeights { get; set; } = new();
}
