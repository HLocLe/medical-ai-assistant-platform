using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class DepartmentRecommendationConfiguration : IEntityTypeConfiguration<DepartmentRecommendation>
{
    public void Configure(EntityTypeBuilder<DepartmentRecommendation> builder)
    {
        builder.ToTable("DepartmentRecommendation");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("RecommendationId").ValueGeneratedOnAdd();

        builder.HasOne(x => x.Department)
            .WithMany(x => x.DepartmentRecommendations)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
