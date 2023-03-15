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
    


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
