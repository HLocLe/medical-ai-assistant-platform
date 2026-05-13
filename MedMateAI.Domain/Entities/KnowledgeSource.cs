namespace MedMateAI.Domain.Entities;

public sealed class KnowledgeSource : BaseEntity
{
    public string? SourceName { get; set; }

    public string? SourceType { get; set; }

    public string? Url { get; set; }

    public int TrustLevel { get; set; }

    public bool IsActive { get; set; }

    public ICollection<KnowledgeDocument> KnowledgeDocuments { get; set; } = new List<KnowledgeDocument>();
}
