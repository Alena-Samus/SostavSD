using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
    public class DrawingConfiguration : IEntityTypeConfiguration<Drawing>
    {
        public void Configure(EntityTypeBuilder<Drawing> builder)
        {
            builder.ToTable("Drawing")
                 .HasKey(k => k.DrawingId);
            builder.Property(p => p.DrawingId);
            builder.Property(p => p.DrawingName)
                .HasMaxLength(200);
            builder.Property(p => p.DrawingPriority);
            builder.Property(p => p.DrawingDateOfAdmissionToDepartmetn);
            builder.Property(p => p.DrawingReleaseDateDepertment);
            builder.Property(p => p.DrawingReleaseDateBySchedule);
            builder.Property(p => p.ProjectId);
            builder.HasOne(o => o.Project)
                .WithMany(m => m.Drawings)
                .HasForeignKey(p => p.ProjectId);
            builder.Property(p => p.StatusId);
            builder.HasOne(o => o.Status)
                .WithMany(m => m.Drawings)
                .HasForeignKey(p => p.StatusId)
                .IsRequired(false);
            builder.Property(p => p.Group);

            builder.HasMany(m => m.Estimates)
                .WithMany(m => m.Drawings)
                .UsingEntity<DrawingEstimate>();

        }
    }
}
