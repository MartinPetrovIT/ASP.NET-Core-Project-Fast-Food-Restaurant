using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Dessert;
using FastFoodResturant.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Controllers
{
    public class DessertController : Controller
    {
        public DessertController(ApplicationDbContext data)
        {
            this.data = data;
        }

        private readonly ApplicationDbContext data;


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddDessertModel dessertFromModel)
        {
            if (!ModelState.IsValid)
            {
                return View(dessertFromModel);

            }

            var dessert = new Dessert()
            {
                Name = dessertFromModel.Name,
                ImageUrl = dessertFromModel.ImageUrl,
                Price = dessertFromModel.Price

            };

            data.Desserts.Add(dessert);

            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


    }
}
