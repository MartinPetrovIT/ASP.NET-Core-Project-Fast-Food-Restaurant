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

namespace FastFoodResturant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext data;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext data)
        {
            _logger = logger;
            this.data = data;
        }

        public IActionResult Index()
        {
            var foods = data.Foods.OrderByDescending(x => x.Id).Select(x => new HomeListingFoodModel
            {
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price

            }).Take(3).ToList();

            return View(foods);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
