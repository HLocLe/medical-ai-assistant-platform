namespace MedMateAI.Domain.Entities;

public sealed class KnowledgeDocument : BaseEntity
{
    public Guid KnowledgeSourceId { get; set; }

    public string? Title { get; set; }

    public string? ContentUrl { get; set; }

    public string? Version { get; set; }

    public DateTime? PublishedAt { get; set; }

    public KnowledgeSource KnowledgeSource { get; set; } = null!;

    public ICollection<KnowledgeChunk> KnowledgeChunks { get; set; } = new List<KnowledgeChunk>();
}
