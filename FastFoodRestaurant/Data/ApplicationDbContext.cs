using FastFoodRestaurant.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastFoodResturant.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Dessert> Desserts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Food>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Foods)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            

           builder.Entity<Food>().Property("Price").HasColumnType("decimal(5, 2)");
           builder.Entity<Drink>().Property("Price").HasColumnType("decimal(5, 2)");
           builder.Entity<Dessert>().Property("Price").HasColumnType("decimal(5, 2)");



            base.OnModelCreating(builder);
        }
    }
}
