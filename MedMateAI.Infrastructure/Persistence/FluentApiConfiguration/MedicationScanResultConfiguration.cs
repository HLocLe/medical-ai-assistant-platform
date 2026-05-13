using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class MedicationScanResultConfiguration : IEntityTypeConfiguration<MedicationScanResult>
{
    public void Configure(EntityTypeBuilder<MedicationScanResult> builder)
    {
        builder.ToTable("MedicationScanResult");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("MedicationScanResultId").ValueGeneratedOnAdd();

        builder.HasOne(x => x.Medicine)
            .WithMany(x => x.MedicationScanResults)
            .HasForeignKey(x => x.MedicineId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
