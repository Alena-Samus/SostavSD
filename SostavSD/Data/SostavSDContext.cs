﻿using Microsoft.EntityFrameworkCore;
using SostavSD.Entities;

namespace SostavSD.Data
{
    public class SostavSDContext: DbContext
    {
        public SostavSDContext(DbContextOptions<SostavSDContext> options) : base(options)
        {
        }

        public DbSet<Contract> contract { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>().ToTable("Contract");

        }
    }
}
