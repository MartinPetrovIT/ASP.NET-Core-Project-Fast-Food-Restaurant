using FastFoodRestaurant.Services.Food;
using FastFoodResturant.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FastFoodResturant.Controllers
{
    public class HomeController : Controller
    {
       
        public HomeController(IFoodService foods)
        {
            this.foods = foods;
        }

        private readonly IFoodService foods;

       public IActionResult Index()
       {
            var foods = this.foods.TakeLastAddedFoods();
            return View(foods);
       }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
