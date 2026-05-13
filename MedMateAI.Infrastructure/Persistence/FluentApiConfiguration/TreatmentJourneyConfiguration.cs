using MedMateAI.Domain.Entities;
using MedMateAI.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class TreatmentJourneyConfiguration : IEntityTypeConfiguration<TreatmentJourney>
{
    public void Configure(EntityTypeBuilder<TreatmentJourney> builder)
    {
        builder.ToTable("TreatmentJourney");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("TreatmentJourneyId").ValueGeneratedOnAdd();

        builder.HasOne<ApplicationUser>()
            .WithMany(x => x.TreatmentJourneys)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.RecoveryPlans)
            .WithOne(x => x.TreatmentJourney)
            .HasForeignKey(x => x.TreatmentJourneyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.AIAnalyses)
            .WithOne(x => x.TreatmentJourney)
            .HasForeignKey(x => x.TreatmentJourneyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.DrugAnalyses)
            .WithOne(x => x.TreatmentJourney)
            .HasForeignKey(x => x.TreatmentJourneyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.UserMedications)
            .WithOne(x => x.TreatmentJourney)
            .HasForeignKey(x => x.TreatmentJourneyId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.FollowUpReminders)
            .WithOne(x => x.TreatmentJourney)
            .HasForeignKey(x => x.TreatmentJourneyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
