using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Crypto.Tls;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
    public class EstimateConfiguration : IEntityTypeConfiguration<Estimate>
    {
        public void Configure(EntityTypeBuilder<Estimate> builder)
        {
            builder.ToTable(nameof(Estimate));
            builder.HasKey(k => k.EstimateId);
            builder.Property(p => p.EstimateNumber);
            builder.Property(p => p.EstimateCode);
            builder.Property(p => p.EstimateName);
            builder.Property(p => p.EstimateReleaseDate);
            builder.Property(p => p.StatusId);
            builder.HasOne(o => o.Status)
                .WithMany(m => m.Estimates)
                .HasForeignKey(o => o.StatusId)
                .IsRequired(false);
            builder.Property(p => p.StatusDate);
            builder.Property(p => p.ReplacementOrAdditionType);
            builder.Property(p => p.ReplacementOrAdditionEsimateId);
            builder.Property(p => p.EstimatorId);
            builder.HasOne(o => o.Estimator)
                .WithMany(m => m.Estimators)
                .HasForeignKey(o => o.EstimatorId)
                .IsRequired(false);
            builder.Property(p => p.SMR);
            builder.Property(p => p.Equipment);
            builder.Property(p => p.Other);
            builder.Property(p => p.Total);

        }
    }
}
