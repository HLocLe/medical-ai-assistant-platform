using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class LabResultDetailConfiguration : IEntityTypeConfiguration<LabResultDetail>
{
    public void Configure(EntityTypeBuilder<LabResultDetail> builder)
    {
        builder.ToTable("LabResultDetails");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("LabResultItemId").ValueGeneratedOnAdd();
    }
}
