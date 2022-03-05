using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Drink;
using FastFoodRestaurant.Services.Drink;
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
