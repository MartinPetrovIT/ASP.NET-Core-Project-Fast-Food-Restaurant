using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Food;
using FastFoodRestaurant.Models.FoodCategory;
using FastFoodRestaurant.Services.Food;
using FastFoodRestaurant.Services.Item;
using FastFoodResturant.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
