using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable(nameof(Status))
                .HasKey(k => k.StatusId);
            builder.Property(p => p.StatusId);
            builder.Property(p => p.StatusName)
                .HasMaxLength(100);
            builder.Property(p => p.IsProject);
            builder.Property(p => p.IsDrawing);
            builder.Property(p => p.IsEstimate);

        }
    }
}
