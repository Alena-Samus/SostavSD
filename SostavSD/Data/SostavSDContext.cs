using Microsoft.EntityFrameworkCore;
using SostavSD.Entities;
using System.Reflection;

namespace SostavSD.Data
{
    public class SostavSDContext: DbContext
    {
        public SostavSDContext(DbContextOptions<SostavSDContext> options) : base(options)
        {
        }

        public DbSet<Company> company { get; set; }
        public DbSet<Contract> contract { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
