using FastFoodRestaurant.Areas.Admin.Models.Drink;
using FastFoodRestaurant.Models.Drink;

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

        int EditDrink(int id,
             string name,
             string imageUrl,
             decimal price,
             bool isAlcoholic);

        int Delete(int id);

        public DrinkFormModel ShowDrinkToEdit(int drinkId);
    }
}
