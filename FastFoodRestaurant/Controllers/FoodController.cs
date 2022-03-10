using FastFoodRestaurant.Models.Food;
using FastFoodRestaurant.Services.Food;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Controllers
{
    public class FoodController : Controller
    {
        public FoodController(IFoodService foods)
        {
            this.foods = foods;

        }


        private readonly IFoodService foods;

        public IActionResult All([FromQuery]FoodSearchModel query)
        {
            
            var foodSearchModel = foods.All(query.SearchTerm, query.Category, query.Sorting, query.CurrentPage);

            return View(foodSearchModel);
        }

    }
}
