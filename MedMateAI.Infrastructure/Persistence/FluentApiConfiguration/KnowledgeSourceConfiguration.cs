using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class KnowledgeSourceConfiguration : IEntityTypeConfiguration<KnowledgeSource>
{
    public void Configure(EntityTypeBuilder<KnowledgeSource> builder)
    {
        builder.ToTable("KnowledgeSource");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("KnowledgeSourceId").ValueGeneratedOnAdd();

        builder.HasMany(x => x.KnowledgeDocuments)
            .WithOne(x => x.KnowledgeSource)
            .HasForeignKey(x => x.KnowledgeSourceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
