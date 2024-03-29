﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            

            builder.ToTable("Contract")
                    .HasKey(k => k.ContractID);
            builder.Property(p => p.Index);
            builder.Property(p => p.Order);
            builder.Property(p => p.ContractNumber);
            builder.Property(p => p.ContractDate);
            builder.Property(p => p.City);
            builder.Property(p => p.CompanyID);
            builder.HasOne(o => o.Company)
                .WithMany(m => m.Contracts)
                .HasForeignKey(p => p.CompanyID)
                .IsRequired();
            builder.Property(p => p.UserID);
            builder.HasOne(o => o.Executor)
                .WithMany(m => m.Contracts) 
                .HasForeignKey(p => p.UserID)
                .IsRequired(false);
            builder.Property(p => p.BuildingZoneId);
            builder.HasOne(o => o.BuildingZone)
                .WithMany(m => m.Contracts)
                .HasForeignKey(p => p.BuildingZoneId);
            builder.Property(p => p.SourceOfFinancingId);
            builder.HasOne(o => o.SourceOfFinacing)
                .WithMany(m => m.Contracts)
                .HasForeignKey(p => p.SourceOfFinancingId);
            

        }
    }
}
