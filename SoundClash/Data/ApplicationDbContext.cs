using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoundClash.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoundClash.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sound> Sound { get; set; }

        // Using existing Conversion of enum into string to save in DB.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Sound>()
                .Property(e => e.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Sound>()
                .HasOne(s => s.Uploader)
                .WithMany(au => au.OwnSounds);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.OwnSounds)
                .WithOne(s => s.Uploader);

/*            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.FavouriteSounds)
                .WithMany(s => s.FavouringUsers);*/
        }
    }
}
