using FastFoodRestaurant.Data.Models;
using FastFoodResturant.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();
            data.Database.Migrate();

            SeedCategories(data);
       
            return app;
        }

        private static void SeedCategories(ApplicationDbContext data)
        {
           if (data.FoodCategories.Any())
           {
               return;
           }

            data.FoodCategories.AddRange(new[]
            {
                new FoodCategory {Name = "Pizzas" },
                new FoodCategory {Name = "Burgers" },
                new FoodCategory {Name = "Chickens" },
                new FoodCategory {Name = "Pasta" },
                new FoodCategory {Name = "Salads" },
                new FoodCategory {Name = "Starters" }



            });

            data.SaveChanges();
        }

        
    }
}
