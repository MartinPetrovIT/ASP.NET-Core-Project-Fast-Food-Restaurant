using FastFoodRestaurant.Areas.Admin.Models.Drink;
using FastFoodRestaurant.Models.Drink;
using Microsoft.AspNetCore.Http;

namespace FastFoodRestaurant.Services.Drink
{
    public interface IDrinkService
    {
        DrinkSearchModel All(
            string searchTerm,
            bool alcoholFreeOnly);

        void Add(
            string name,
            decimal price,
            IFormFile image,
            bool isAlcoholic,
            int itemId);

        int EditDrink(int id,
              string name,
            decimal price,
            IFormFile image,
            bool isAlcoholic);

        int Delete(int id);

        public DrinkFormModel ShowDrinkToEdit(int drinkId);
    }
}
