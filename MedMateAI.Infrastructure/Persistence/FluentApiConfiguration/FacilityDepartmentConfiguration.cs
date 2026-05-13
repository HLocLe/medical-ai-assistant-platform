using MedMateAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class FacilityDepartmentConfiguration : IEntityTypeConfiguration<FacilityDepartment>
{
    public void Configure(EntityTypeBuilder<FacilityDepartment> builder)
    {
        builder.ToTable("FacilityDepartment");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("FacilityDepartmentId").ValueGeneratedOnAdd();

        builder.HasIndex(x => new { x.FacilityId, x.DepartmentId }).IsUnique();

        builder.HasOne(x => x.Facility)
            .WithMany(x => x.FacilityDepartments)
            .HasForeignKey(x => x.FacilityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Department)
            .WithMany(x => x.FacilityDepartments)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
