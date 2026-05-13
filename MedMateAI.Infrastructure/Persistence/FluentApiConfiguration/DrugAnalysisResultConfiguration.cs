using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class DrugAnalysisResultConfiguration : IEntityTypeConfiguration<DrugAnalysisResult>
{
    public void Configure(EntityTypeBuilder<DrugAnalysisResult> builder)
    {
        builder.ToTable("DrugAnalysisResult");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("DrugAnalysisResultId").ValueGeneratedOnAdd();

        builder.HasOne(x => x.Medicine)
            .WithMany(x => x.DrugAnalysisResults)
            .HasForeignKey(x => x.MedicineId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
