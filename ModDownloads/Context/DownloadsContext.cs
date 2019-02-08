﻿using Microsoft.EntityFrameworkCore;
using ModDownloads.Shared.Entities;

namespace ModDownloads.Server.Context
{
    public class DownloadsContext : DbContext
    {

        public DbSet<Download> Download { get; set; }

        public DbSet<Mod> Mod { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  optionsBuilder.UseMySQL("server=localhost;database=mod_downloads;user=root;password=;");
            optionsBuilder.UseSqlite("Data Source=downloads.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Mod>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.URL).IsRequired();
            });

            modelBuilder.Entity<Download>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Timestamp);
                entity.HasOne(d => d.Mod)
                  .WithMany(p => p.Downloads);
            });
        }
    }
}