using Microsoft.EntityFrameworkCore;
using RuneAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuneAPI.Database
{
    public class RuneDbContext : DbContext
    {
        public DbSet<Rune> Runes { get; set; }
        public DbSet<Runeword> Runewords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Environment.GetEnvironmentVariable("SQLITE_DB_PATH");

            if (string.IsNullOrWhiteSpace(dbPath))
            {
                dbPath = "runes.db";
            }

            optionsBuilder.UseSqlite($"DataSource={dbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Runeword>().HasMany(r => r.Runes).WithMany("RUNEWORDS");
        }
    }
}
