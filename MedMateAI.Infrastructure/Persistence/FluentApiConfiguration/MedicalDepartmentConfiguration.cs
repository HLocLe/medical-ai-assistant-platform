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

        builder.Property(x => x.ChapterCode)
            .HasMaxLength(10);

        builder.HasIndex(x => x.ChapterCode);

        builder.HasOne(x => x.IcdChapter)
            .WithMany(x => x.MedicalDepartments)
            .HasForeignKey(x => x.ChapterCode)
            .HasPrincipalKey(x => x.ChapterCode)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
