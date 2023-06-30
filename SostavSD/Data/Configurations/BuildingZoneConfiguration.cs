using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
	public class BuildingZoneConfiguration : IEntityTypeConfiguration<BuildingZone>
	{
		public void Configure(EntityTypeBuilder<BuildingZone> builder)
		{
			builder.ToTable(nameof(BuildingZone))
				.HasKey(k => k.BuildingZoneId);
			builder.Property(k => k.BuildingZoneId);
			builder.Property(p => p.BuildingZoneName)
				.HasMaxLength(20);
		}
	}
}
