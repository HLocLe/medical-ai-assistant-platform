using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class ConsultationQuestionConfiguration : IEntityTypeConfiguration<ConsultationQuestion>
{
    public void Configure(EntityTypeBuilder<ConsultationQuestion> builder)
    {
        builder.ToTable("ConsultationQuestion");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("QuestionId").ValueGeneratedOnAdd();
    }
}
