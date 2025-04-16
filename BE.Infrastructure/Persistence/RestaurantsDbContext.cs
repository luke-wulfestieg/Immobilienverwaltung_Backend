using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Persistence
{
    internal class RestaurantsDbContext : DbContext
    {
        internal DbSet<Restaurant> Restaurants { get; set; }

        internal DbSet<Dish> Dishes { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) : base
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=Restaurant;Trusted_Connection=True;TrustServerCertificate=True;");
        //}

        public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>()
                .OwnsOne(r => r.Address);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Dishes)
                .WithOne()
                .HasForeignKey(d => d.RestaurantId);

        }
    }
}
