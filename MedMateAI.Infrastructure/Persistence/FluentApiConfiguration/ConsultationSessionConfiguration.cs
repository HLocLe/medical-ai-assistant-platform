using MedMateAI.Domain.Entities;
using MedMateAI.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class ConsultationSessionConfiguration : IEntityTypeConfiguration<ConsultationSession>
{
    public void Configure(EntityTypeBuilder<ConsultationSession> builder)
    {
        builder.ToTable("ConsultationSession");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ConsultationSessionId").ValueGeneratedOnAdd();

        builder.HasOne<ApplicationUser>()
            .WithMany(x => x.ConsultationSessions)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Facility)
            .WithMany(x => x.ConsultationSessions)
            .HasForeignKey(x => x.FacilityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Department)
            .WithMany(x => x.ConsultationSessions)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Doctor)
            .WithMany(x => x.ConsultationSessions)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.ConsultationQuestions)
            .WithOne(x => x.ConsultationSession)
            .HasForeignKey(x => x.ConsultationSessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
