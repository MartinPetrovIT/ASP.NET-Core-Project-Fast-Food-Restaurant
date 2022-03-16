using FastFoodRestaurant.Services.Drink;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Controllers
{
    public class DrinkController : Controller
    {

        public DrinkController(IDrinkService drinks)
        {
            this.drinks = drinks;
        }

       
        private readonly IDrinkService drinks;

       
        public IActionResult All(
            string searchTerm,
            bool alcoholFreeOnly)
        {

            var allDrinks = drinks.All(searchTerm, alcoholFreeOnly);
            return View(allDrinks);          
        }

    }
}
