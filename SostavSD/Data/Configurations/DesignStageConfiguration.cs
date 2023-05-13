using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
    public class DesignStageConfiguration : IEntityTypeConfiguration<DesignStage>
    {
        public void Configure(EntityTypeBuilder<DesignStage> builder)
        {
            builder.ToTable("DesignStage")
                .HasKey(k => k.StageId);
            builder.Property(p => p.StageId);
            builder.Property(p => p.StageName)
                .HasMaxLength(100);
        }
    }
}
