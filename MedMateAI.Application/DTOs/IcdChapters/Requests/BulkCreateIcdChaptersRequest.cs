namespace MedMateAI.Application.DTOs.IcdChapters.Requests;

public sealed class BulkCreateIcdChaptersRequest
{
    public IList<CreateIcdChapterRequest> Chapters { get; set; } = new List<CreateIcdChapterRequest>();
}
