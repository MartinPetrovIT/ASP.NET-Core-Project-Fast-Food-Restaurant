using FastFoodRestaurant.Data.Models;
using Microsoft.AspNetCore.Identity;
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

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<FoodCategory> FoodCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Food>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Foods)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<Client>()
              .HasOne<IdentityUser>()
              .WithOne()
              .HasForeignKey<Client>(d => d.UserId);

            builder
             .Entity<Client>()
             .HasOne<Cart>()
             .WithOne()
             .HasForeignKey<Client>(d => d.CartId);




            builder.Entity<Food>().Property("Price").HasColumnType("decimal(5, 2)");
           builder.Entity<Drink>().Property("Price").HasColumnType("decimal(5, 2)");



            base.OnModelCreating(builder);
        }
    }
}
