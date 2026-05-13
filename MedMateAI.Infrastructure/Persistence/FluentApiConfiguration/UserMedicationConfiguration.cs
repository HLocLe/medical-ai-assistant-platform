using MedMateAI.Domain.Entities;
using MedMateAI.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class UserMedicationConfiguration : IEntityTypeConfiguration<UserMedication>
{
    public void Configure(EntityTypeBuilder<UserMedication> builder)
    {
        builder.ToTable("UserMedication");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("UserMedicationId").ValueGeneratedOnAdd();

        builder.HasOne<ApplicationUser>()
            .WithMany(x => x.UserMedications)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Medicine)
            .WithMany(x => x.UserMedications)
            .HasForeignKey(x => x.MedicineId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
