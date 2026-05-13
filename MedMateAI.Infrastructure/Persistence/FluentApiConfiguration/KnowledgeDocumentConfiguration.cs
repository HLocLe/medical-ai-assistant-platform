using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class KnowledgeDocumentConfiguration : IEntityTypeConfiguration<KnowledgeDocument>
{
    public void Configure(EntityTypeBuilder<KnowledgeDocument> builder)
    {
        builder.ToTable("KnowledgeDocument");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("KnowledgeDocumentId").ValueGeneratedOnAdd();

        builder.HasMany(x => x.KnowledgeChunks)
            .WithOne(x => x.KnowledgeDocument)
            .HasForeignKey(x => x.KnowledgeDocumentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
