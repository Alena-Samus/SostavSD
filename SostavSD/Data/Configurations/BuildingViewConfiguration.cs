using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
    public class BuildingViewConfiguration : IEntityTypeConfiguration<BuildingView>
    {
        void IEntityTypeConfiguration<BuildingView>.Configure(EntityTypeBuilder<BuildingView> builder)
        {
            builder.ToTable(nameof(BuildingView))
                .HasKey(k => k.BuildingViewId);
            builder.Property(p => p.BuildingViewId);
            builder.Property(p => p.BuildingViewName)
                .HasMaxLength(50);
        }
    }
}
