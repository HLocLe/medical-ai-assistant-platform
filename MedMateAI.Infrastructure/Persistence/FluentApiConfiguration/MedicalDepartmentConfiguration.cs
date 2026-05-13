using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class MedicalDepartmentConfiguration : IEntityTypeConfiguration<MedicalDepartment>
{
    public void Configure(EntityTypeBuilder<MedicalDepartment> builder)
    {
        builder.ToTable("MedicalDepartment");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("DepartmentId").ValueGeneratedOnAdd();
    }
}
