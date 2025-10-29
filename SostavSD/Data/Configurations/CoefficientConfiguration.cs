using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
    public class CoefficientConfiguration: IEntityTypeConfiguration<Coefficient>
    {
        public void Configure(EntityTypeBuilder<Coefficient> builder)
        {
            builder.ToTable("Coefficient")
                .HasKey(k => k.CoefficientId);
            builder.Property(p => p.CoefficientId);
            builder.Property(p => p.CoefficientName);
            builder.Property(p => p.Qualifier)
                .HasMaxLength(5);
            builder.Property(p => p.OHROPR1);
            builder.Property(p => p.PlannedProfit);
            builder.Property(p => p.OHROPR2);
            builder.Property(p => p.Relevance);
            
            builder.Property(p => p.BuildingViewId);      
            builder.HasOne(o => o.BuildingView)
                .WithMany(m => m.Coefficients)
                .HasForeignKey(p => p.BuildingViewId)
                .IsRequired(false);

            builder.Property(p => p.BuildingZoneId);
            builder.HasOne(o => o.BuildingZone)
                .WithMany(m => m.Coefficients)
                .HasForeignKey(p => p.BuildingZoneId)
                .IsRequired(false);
           
        }
    }
}
