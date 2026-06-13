using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class SessionClinicalQuestionAnswerConfiguration : IEntityTypeConfiguration<SessionClinicalQuestionAnswer>
{
    public void Configure(EntityTypeBuilder<SessionClinicalQuestionAnswer> builder)
    {
        builder.ToTable("SessionClinicalQuestionAnswer");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("SessionClinicalQuestionAnswerId");
           

        builder.Property(x => x.Answer)
            .IsRequired();

        builder.HasIndex(x => new { x.SymptomAnalysisSessionId, x.ClinicalQuestionId })
            .IsUnique();

        builder.HasOne(x => x.SymptomAnalysisSession)
            .WithMany(x => x.ClinicalQuestionAnswers)
            .HasForeignKey(x => x.SymptomAnalysisSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ClinicalQuestion)
            .WithMany(x => x.SessionAnswers)
            .HasForeignKey(x => x.ClinicalQuestionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
