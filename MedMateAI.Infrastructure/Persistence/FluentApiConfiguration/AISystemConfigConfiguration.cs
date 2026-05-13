using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class AISystemConfigConfiguration : IEntityTypeConfiguration<AISystemConfig>
{
    public void Configure(EntityTypeBuilder<AISystemConfig> builder)
    {
        builder.ToTable("AISystemConfig");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ConfigId").ValueGeneratedOnAdd();

        builder.HasIndex(x => x.TaskType).IsUnique();

        builder.Property(x => x.TaskType).HasMaxLength(128).IsRequired();

        builder.HasOne(x => x.SymptomAnalysisSession)
            .WithMany(x => x.AISystemConfigs)
            .HasForeignKey(x => x.SymptomAnalysisSessionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.MedicationScan)
            .WithMany(x => x.AISystemConfigs)
            .HasForeignKey(x => x.MedicationScanId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Visit)
            .WithMany(x => x.AISystemConfigs)
            .HasForeignKey(x => x.VisitId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.RecoveryPlan)
            .WithMany(x => x.AISystemConfigs)
            .HasForeignKey(x => x.RecoveryPlanId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.DrugAnalysis)
            .WithMany(x => x.AISystemConfigs)
            .HasForeignKey(x => x.DrugAnalysisId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.ConsultationSession)
            .WithMany(x => x.AISystemConfigs)
            .HasForeignKey(x => x.ConsultationSessionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
