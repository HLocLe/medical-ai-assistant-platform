using MedMateAI.Domain.Entities;
using MedMateAI.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class MedicationScanConfiguration : IEntityTypeConfiguration<MedicationScan>
{
    public void Configure(EntityTypeBuilder<MedicationScan> builder)
    {
        builder.ToTable("MedicationScan");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("MedicationScanId").ValueGeneratedOnAdd();

        builder.HasOne<ApplicationUser>()
            .WithMany(x => x.MedicationScans)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.MedicationScanResults)
            .WithOne(x => x.MedicationScan)
            .HasForeignKey(x => x.MedicationScanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
