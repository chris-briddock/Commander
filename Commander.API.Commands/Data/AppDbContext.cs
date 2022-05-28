using System;
using System.Collections.Generic;
using Commander.API.Commands.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Commander.API.Commands.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Command> Commands { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:AzureConnection");
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            for (long i = 0; i < 90000; i++)
            {
                modelBuilder.Entity<Command>().HasData(new Command
                {
                    Id = Guid.NewGuid(),
                    OperatingSystem = "Sample OS",
                    CommandString = "This is gonna take ages",
                    Parameters = "I'm an AI avatar now",
                    ParametersSummary = "Whats in your head, in your head zombie.",
                    RuntimeEnvironment = "Aaaaaabc smart ass done graduated a live time of school and we glad you made it."
                });
            }

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
