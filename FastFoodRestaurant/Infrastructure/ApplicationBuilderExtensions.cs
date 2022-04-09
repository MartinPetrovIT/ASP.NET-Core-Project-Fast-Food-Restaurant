using FastFoodRestaurant.Areas.Admin;
using FastFoodRestaurant.Data.Models;
using FastFoodResturant.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using static FastFoodRestaurant.WebConstants;

namespace FastFoodRestaurant.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopedServices.ServiceProvider;

            var data = serviceProvider.GetRequiredService<ApplicationDbContext>();
            data.Database.Migrate();

            SeedCategories(data);
            SeedAdministrator(serviceProvider);
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
                new FoodCategory {Name = "Desserts" }



            });

            data.SaveChanges();
        }

       

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<Client>>();

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


            Task.Run(async () =>
           {
               if (await roleManager.RoleExistsAsync(AdminConstants.Administrator))
               {
                   return;
               }
               var role = new IdentityRole { Name = AdminConstants.Administrator };

               await roleManager.CreateAsync(role);
               const string adminEmail = "admin@abv.bg";

               const string adminPassword = "123456";
               var user = new Client
               {
                   Email = adminEmail,
                   UserName = adminEmail
               };
               await userManager.CreateAsync(user, adminPassword);

               await userManager.AddToRoleAsync(user, role.Name);
           })
                .GetAwaiter()
                .GetResult();
        }


    }
}
