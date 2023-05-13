using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project")
                .HasKey(k => k.ProjectId);
            builder.Property(p => p.ProjectId);
            builder.Property(p => p.BuildingNumber);
            builder.Property(p => p.Priority)
                .HasMaxLength(3);
            builder.Property(p => p.ContractId);
            builder.HasOne(o => o.Contract)
                .WithMany(m => m.Projects)
                .HasForeignKey(p => p.ProjectId)
                .IsRequired(false);
            builder.Property(p => p.BuildingViewId);
            builder.HasOne(o => o.BuildingView)
                .WithMany(m => m.Projects)
                .HasForeignKey(p => p.ProjectId)
                .IsRequired(false);
            builder.Property(p => p.ProjectReleaseDate);
            builder.Property(p => p.ProjectReleaseDateByContract);
            builder.Property(p => p.WorkStartDate);
            builder.Property(p => p.PriceLevel);
            builder.Property(p => p.PrintType)
                .HasMaxLength(25);
            builder.Property(p => p.CiCVersion).HasMaxLength(20);
            builder.Property(p => p.ConstructionPhase)
                .HasMaxLength(50);

        }
    }
}
