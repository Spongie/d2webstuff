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
        public virtual DbSet<Modifier> Modifiers { get; set; }
        public virtual DbSet<Rune> Runes { get; set; }
        public virtual DbSet<Runeword> Runewords { get; set; }
        public virtual DbSet<RunewordRune> RunewordRunes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=runes.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modifier>(entity =>
            {
                entity.HasIndex(e => e.RunewordId, "IX_Modifier_RunewordId");

                entity.HasOne(d => d.Runeword)
                    .WithMany(p => p.Modifiers)
                    .HasForeignKey(d => d.RunewordId);
            });

            modelBuilder.Entity<RunewordRune>(entity =>
            {
                entity.HasOne(d => d.Rune)
                    .WithMany(p => p.RunewordRunes)
                    .HasForeignKey(d => d.RuneId);

                entity.HasOne(d => d.Runeword)
                    .WithMany(p => p.RunewordRunes)
                    .HasForeignKey(d => d.RunewordId);
            });
        }
    }
}
