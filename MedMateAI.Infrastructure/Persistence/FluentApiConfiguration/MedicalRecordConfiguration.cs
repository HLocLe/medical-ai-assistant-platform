using MedMateAI.Domain.Entities;
using MedMateAI.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.ToTable("MedicalRecord");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("MedicalRecordId").ValueGeneratedOnAdd();

        builder.HasOne<ApplicationUser>()
            .WithMany(x => x.MedicalRecords)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Visit)
            .WithMany(x => x.MedicalRecords)
            .HasForeignKey(x => x.VisitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.MedicalRecordFiles)
            .WithOne(x => x.MedicalRecord)
            .HasForeignKey(x => x.MedicalRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.LabResults)
            .WithOne(x => x.MedicalRecord)
            .HasForeignKey(x => x.MedicalRecordId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
