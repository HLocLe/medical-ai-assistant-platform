using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class MedicalRecordFileConfiguration : IEntityTypeConfiguration<MedicalRecordFile>
{
    public void Configure(EntityTypeBuilder<MedicalRecordFile> builder)
    {
        builder.ToTable("MedicalRecordFile");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("MedicalRecordFileId").ValueGeneratedOnAdd();
    }
}
