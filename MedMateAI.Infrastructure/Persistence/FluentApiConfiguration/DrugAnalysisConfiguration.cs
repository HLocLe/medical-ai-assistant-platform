using MedMateAI.Domain.Entities;
using MedMateAI.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class DrugAnalysisConfiguration : IEntityTypeConfiguration<DrugAnalysis>
{
    public void Configure(EntityTypeBuilder<DrugAnalysis> builder)
    {
        builder.ToTable("DrugAnalysis");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("DrugAnalysisId").ValueGeneratedOnAdd();

        builder.HasOne<ApplicationUser>()
            .WithMany(x => x.DrugAnalyses)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.DrugAnalysisResults)
            .WithOne(x => x.DrugAnalysis)
            .HasForeignKey(x => x.DrugAnalysisId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
