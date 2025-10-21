using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
    public class SubsectionConfiguration : IEntityTypeConfiguration<Subsection>
    {
        void IEntityTypeConfiguration<Subsection>.Configure(EntityTypeBuilder<Subsection> builder)
        {
            builder.ToTable(nameof(Subsection))
                .HasKey(k => k.SubsectionId);
            builder.Property(p => p.SubsectionId);
            builder.Property(p => p.SerialNumber);
            builder.Property(p => p.SubsectionName);
            builder.Property(p => p.ChapterId);
            builder.HasOne(O => O.Chapter)
                .WithMany(m => m.Sections)
                .HasForeignKey(p => p.ChapterId)
                .IsRequired(false);
            builder.Property(p => p.ProjectId);
            builder.HasOne(o => o.Project)
                .WithMany(m => m.Sections)
                .HasForeignKey(o => o.ProjectId)
                .IsRequired(false);
            builder.Property(p => p.K1)
                .HasMaxLength(80);
            builder.Property(p => p.K2)
                .HasMaxLength(80);
            builder.Property(p => p.Norm)
                .HasMaxLength(80);
            builder.Property(p => p.Notes);

        }
    }
}
