using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Food;
using FastFoodResturant.Data;
using FastFoodResturant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FastFoodRestaurant.Models.Home;
using FastFoodRestaurant.Services.Food;

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
