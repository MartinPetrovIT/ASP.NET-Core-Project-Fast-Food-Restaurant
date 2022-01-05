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

            //SeedCategories(data);
            //SeedIngredients(data);
       
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
                new FoodCategory {Name = "Fishes" },
                new FoodCategory {Name = "Pastas" },
                new FoodCategory {Name = "Salads" },
                new FoodCategory {Name = "Starters" }



            });

            data.SaveChanges();
        }

        private static void SeedIngredients(ApplicationDbContext data)
        {
            if (data.Ingredients.Any())
            {
                return;
            }

            data.Ingredients.AddRange(new[]
            {
                new Ingredient { Name = "tomatoes" , IsSpicy = false },
                new Ingredient { Name = "green peppers" , IsSpicy = false },
                new Ingredient { Name = "onions" , IsSpicy = true },
                new Ingredient { Name = "jalapeno peppers" , IsSpicy = true },
                new Ingredient { Name = "yellow cheese" , IsSpicy = false },
                new Ingredient { Name = "white cheese" , IsSpicy = false },
                new Ingredient { Name = "iceberg lettuce" , IsSpicy = false },
                new Ingredient { Name = "croutons" , IsSpicy = false },
                new Ingredient { Name = "corn" , IsSpicy = false },
                new Ingredient { Name = "chicken" , IsSpicy = false },
                new Ingredient { Name = "bacon" , IsSpicy = false },
                new Ingredient { Name = "Caesar dressing" , IsSpicy = false },
                new Ingredient { Name = "tuna" , IsSpicy = false },
                new Ingredient { Name = "olives" , IsSpicy = false },
                new Ingredient { Name = "olive oil" , IsSpicy = false },
                new Ingredient { Name = "tomato sauce" , IsSpicy = false },
                new Ingredient { Name = "mozzarella" , IsSpicy = false },
                new Ingredient { Name = "pepperoni" , IsSpicy = false },
                new Ingredient { Name = "spicy beef" , IsSpicy = true },
                new Ingredient { Name = "barbecue sauce" , IsSpicy = false },
                new Ingredient { Name = "pineapple" , IsSpicy = false },
                new Ingredient { Name = "ham" , IsSpicy = false },

            });

            data.SaveChanges();
        }
    }
}
