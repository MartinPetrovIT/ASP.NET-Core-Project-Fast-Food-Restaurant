using FastFoodRestaurant.Models.Drink;
using FastFoodResturant.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Services.Drink
{
    public class DrinkService : IDrinkService
    {
        public DrinkService(ApplicationDbContext data)
        {
            this.data = data;
        }
        private readonly ApplicationDbContext data;
        public DrinkSearchModel All(string searchTerm, bool alcoholFreeOnly)
        {
            var drinkQuery = data.Drinks.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                drinkQuery = drinkQuery.Where(f =>
                f.Name.ToLower().Contains(searchTerm.ToLower())
                );

            }
            if (alcoholFreeOnly == true)
            {
                drinkQuery = drinkQuery.Where(x => x.IsAlcoholic == false);
            }

            var drinks = drinkQuery.Select(x => new DrinkListingModel
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                IsAlcoholic = x.IsAlcoholic,
                ItemId = x.ItemId

            }).ToList();


            return new DrinkSearchModel
            {
                Drink = drinks,
                SearchTerm = searchTerm,
                AlcoholFreeOnly = alcoholFreeOnly
            };
        }

        void IDrinkService.Add(
            string name,
            string imageUrl, 
            decimal price, 
            bool isAlcoholic, 
            int itemId)
        {
            var drink = new Data.Models.Drink()
            {
                Name = name,
                ImageUrl = imageUrl,
                Price = price,
                IsAlcoholic = isAlcoholic,
                ItemId = itemId

            };



            data.Drinks.Add(drink);



            data.SaveChanges();
        }
    }
}
