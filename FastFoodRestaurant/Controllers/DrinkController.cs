using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Drink;
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

        public DrinkController(ApplicationDbContext data)
        {
            this.data = data;
        }

        private readonly ApplicationDbContext data;

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddDrinkModel drinkFromModel)
        {
            if (!ModelState.IsValid)
            {
                return View(drinkFromModel);

            }
            var item = new Item()
            {              
                Name = drinkFromModel.Name,
                Price = drinkFromModel.Price
            };
            data.Items.Add(item);
            data.SaveChanges();

            var drink = new Drink()
            {
                Name = drinkFromModel.Name,
                ImageUrl = drinkFromModel.ImageUrl,
                Price = drinkFromModel.Price,
                IsAlcoholic = drinkFromModel.IsAlcoholic,
                ItemId = item.Id

            };


          
            data.Drinks.Add(drink);
          


            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All(
            string searchTerm,
            bool alcoholFreeOnly)
        {
            var drinkQuery = data.Drinks.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                drinkQuery = drinkQuery.Where(f =>
                f.Name.ToLower().Contains(searchTerm.ToLower())
                );

            }
            if (alcoholFreeOnly == true)
            {
                drinkQuery = drinkQuery.Where(x => x.IsAlcoholic == false);
            }
           
            var drinks = drinkQuery.Select(x => new DrinkListingModel
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                IsAlcoholic = x.IsAlcoholic,
                ItemId = x.ItemId

            }).ToList();


            return View(new DrinkSearchModel
            {
                Drink = drinks,
                SearchTerm = searchTerm,
                AlcoholFreeOnly = alcoholFreeOnly


            });
        }

    }
}
