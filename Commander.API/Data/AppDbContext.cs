using System;
using Microsoft.EntityFrameworkCore;
using Commander.API.Models;
using System.Diagnostics.CodeAnalysis;

namespace Commander.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }
        public DbSet<Command> ?Commands { get; set; }
    }
}