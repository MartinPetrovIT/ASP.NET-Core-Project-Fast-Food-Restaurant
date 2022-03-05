using FastFoodRestaurant.Areas.Admin.Models.Drink;
using FastFoodRestaurant.Services.Drink;
using FastFoodRestaurant.Services.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Areas.Admin.Controllers
{
    
    public class DrinkController : AdminController
    {
        public DrinkController(IItemService items, IDrinkService drinks)
        {
            this.items = items;
            this.drinks = drinks;
        }


        private readonly IItemService items;
        private readonly IDrinkService drinks;

        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Add(DrinkFormModel drinkFromModel)
        {
            if (!ModelState.IsValid)
            {
                return View(drinkFromModel);

            }

            var itemId = items.Add(drinkFromModel.Name, drinkFromModel.Price);

            drinks.Add(
                drinkFromModel.Name,
                drinkFromModel.ImageUrl,
                drinkFromModel.Price,
                drinkFromModel.IsAlcoholic,
                itemId);


            return RedirectToAction("Index", "Home");
        }

    }
}
