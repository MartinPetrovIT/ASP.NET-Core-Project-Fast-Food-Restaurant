using FastFoodRestaurant;
using FastFoodRestaurant.Models.Home;
using FastFoodRestaurant.Services.Food;
using FastFoodResturant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static FastFoodRestaurant.WebConstants.Cache;
namespace FastFoodResturant.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(IFoodService foods, IMemoryCache cache)
        {
            this.foods = foods;
            this.cache = cache;
        }

        private readonly IFoodService foods;
        private readonly IMemoryCache cache;

        public IActionResult Index()
        {
            var foods = cache.Get<List<HomeListingFoodModel>>(LatestFoodsCacheKey);

            if (foods is null)
            {
                foods = this.foods.TakeLastAddedFoods().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(LatestFoodsCacheKey, foods, cacheOptions);
            }


            return View(foods);
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
