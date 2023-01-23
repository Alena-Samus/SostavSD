using SostavSD.Models;
using Microsoft.EntityFrameworkCore;

namespace SostavSD.Data
{
    public class SostavSDContext: DbContext
    {
        public SostavSDContext(DbContextOptions<SostavSDContext> options) : base(options)
        {
        }

        public DbSet<ContractModel> Contract { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContractModel>().ToTable("Contract");
  
        }
    }
}
