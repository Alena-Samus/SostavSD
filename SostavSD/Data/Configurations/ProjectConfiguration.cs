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
            builder.Property(p => p.Priority);
            builder.Property(p => p.ContractId);
            builder.HasOne(o => o.Contract)
                .WithMany(m => m.Projects)
                .HasForeignKey(p => p.ContractId)
                .IsRequired(false);
            builder.Property(p => p.ConstructionPhase);
            builder.Property(p => p.StageId);
            builder.Property(p => p.BuildingViewId);
            builder.HasOne(o => o.BuildingView)
                .WithMany(m => m.Projects)
                .HasForeignKey(p => p.BuildingViewId) 
                .IsRequired(false); 
            builder.Property(p => p.ProjectReleaseDate);
            builder.Property(p => p.ProjectReleaseDateByContract);
            builder.Property(p => p.WorkStartDate);
            builder.Property(p => p.PriceLevel);
            builder.Property(p => p.PrintType);
            builder.Property(p => p.CiCVersion);
        }
    }
}
