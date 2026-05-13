using MedMateAI.Domain.Entities;
using MedMateAI.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class AIAnalysisConfiguration : IEntityTypeConfiguration<AIAnalysis>
{
    public void Configure(EntityTypeBuilder<AIAnalysis> builder)
    {
        builder.ToTable("AIAnalysis");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("AIAnalysisId").ValueGeneratedOnAdd();

        builder.HasOne<ApplicationUser>()
            .WithMany(x => x.AIAnalyses)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
