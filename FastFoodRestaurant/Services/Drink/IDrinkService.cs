using FastFoodRestaurant.Models.Drink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Services.Drink
{
    public interface IDrinkService
    {
        DrinkSearchModel All(
            string searchTerm,
            bool alcoholFreeOnly);

        void Add(
            string name,
            string imageUrl,
            decimal price,
            bool isAlcoholic,
            int itemId);
       
    }
}
