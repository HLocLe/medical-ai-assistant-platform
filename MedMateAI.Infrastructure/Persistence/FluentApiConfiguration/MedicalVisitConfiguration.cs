using MedMateAI.Domain.Entities;
using MedMateAI.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class MedicalVisitConfiguration : IEntityTypeConfiguration<MedicalVisit>
{
    public void Configure(EntityTypeBuilder<MedicalVisit> builder)
    {
        builder.ToTable("MedicalVisit");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("VisitId").ValueGeneratedOnAdd();

        builder.HasOne<ApplicationUser>()
            .WithMany(x => x.MedicalVisits)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Facility)
            .WithMany(x => x.MedicalVisits)
            .HasForeignKey(x => x.FacilityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Department)
            .WithMany(x => x.MedicalVisits)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Doctor)
            .WithMany(x => x.MedicalVisits)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.TreatmentJourneys)
            .WithOne(x => x.Visit)
            .HasForeignKey(x => x.VisitId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
