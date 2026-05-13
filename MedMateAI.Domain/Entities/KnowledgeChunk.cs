namespace MedMateAI.Domain.Entities;

public sealed class KnowledgeChunk : BaseEntity
{
    public Guid KnowledgeDocumentId { get; set; }

    public string? ChunkText { get; set; }

    public string? EmbeddingVectorReference { get; set; }

    public int? PageNumber { get; set; }

    public KnowledgeDocument KnowledgeDocument { get; set; } = null!;
}
