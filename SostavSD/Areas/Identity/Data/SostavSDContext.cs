using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SostavSD.Entities;
using System.Reflection.Emit;
using System.Reflection;

namespace SostavSD.Data;

public class SostavSDContext : IdentityDbContext<UserSostav>
{
    public SostavSDContext(DbContextOptions<SostavSDContext> options)
        : base(options)
    { 
    }
	public DbSet<Company> company { get; set; }
	public DbSet<Contract> contract { get; set; }
    public DbSet<BuildingZone> buildingZone { get; set; }
    public DbSet<SourceOfFinacing> sourceOfFinacing { get; set; }
    public DbSet<BuildingView> buildingView { get; set; }  
    public DbSet<Project> project { get; set; }
    public DbSet<DesignStage> designStage { get; set; }
  


	protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
