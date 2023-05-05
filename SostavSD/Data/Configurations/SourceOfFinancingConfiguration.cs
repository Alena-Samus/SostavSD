using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
	public class SourceOfFinancingConfiguration : IEntityTypeConfiguration<SourceOfFinacing>
	{
		public void Configure(EntityTypeBuilder<SourceOfFinacing> builder)
		{
			builder.ToTable("SourceOfFinancing")
				.HasKey(k => k.SourceId);
			builder.Property(p => p.SourceId);
			builder.Property(p => p.SourceName);
		}
	}
}
