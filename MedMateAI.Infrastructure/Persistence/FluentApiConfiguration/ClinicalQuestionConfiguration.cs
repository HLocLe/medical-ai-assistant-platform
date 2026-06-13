using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class ClinicalQuestionConfiguration : IEntityTypeConfiguration<ClinicalQuestion>
{
    public void Configure(EntityTypeBuilder<ClinicalQuestion> builder)
    {
        builder.ToTable("ClinicalQuestions");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("QuestionId").ValueGeneratedOnAdd();

        builder.Property(x => x.ChapterId);

        builder.Property(x => x.ChapterCode)
            .HasMaxLength(10);

        builder.Property(x => x.QuestionVi)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.EnglishPrefix)
            .HasMaxLength(500);

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.HasIndex(x => new { x.ChapterId, x.SortOrder });

        builder.HasOne(x => x.IcdChapter)
            .WithMany(x => x.ClinicalQuestions)
            .HasForeignKey(x => x.ChapterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
