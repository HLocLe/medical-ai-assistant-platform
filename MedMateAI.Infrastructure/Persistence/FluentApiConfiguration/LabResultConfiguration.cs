using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class LabResultConfiguration : IEntityTypeConfiguration<LabResult>
{
    public void Configure(EntityTypeBuilder<LabResult> builder)
    {
        builder.ToTable("LabResult");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("LabResultId").ValueGeneratedOnAdd();

        builder.HasMany(x => x.LabResultDetails)
            .WithOne(x => x.LabResult)
            .HasForeignKey(x => x.LabResultId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
