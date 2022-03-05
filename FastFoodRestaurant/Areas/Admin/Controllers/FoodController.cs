using FastFoodRestaurant.Areas.Admin.Models.Food;
using FastFoodRestaurant.Services.Food;
using FastFoodRestaurant.Services.Item;
using FastFoodResturant.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Areas.Admin.Controllers
{
   
    public class FoodController : AdminController
    {

        public FoodController(ApplicationDbContext data, IFoodService foods, IItemService items)
        {
            this.foods = foods;
            this.data = data;
            this.items = items;
        }

        private readonly ApplicationDbContext data;
        private readonly IFoodService foods;
        private readonly IItemService items;

        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Add()
        {
            return View(new FoodFormModel { Categories = foods.GetFoodCategories()});
        }

        [HttpPost]
        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Add(FoodFormModel foodFromModel)
        {
            if (!this.data.FoodCategories.Any(c => c.Id == foodFromModel.CategoryId))
            {
                ModelState.AddModelError(nameof(foodFromModel), "Category does not exist!");
            }
            if (!ModelState.IsValid)
            {
                foodFromModel.Categories = foods.GetFoodCategories();
                return View(foodFromModel);
            }


            var itemId = items.Add(foodFromModel.Name, foodFromModel.Price);


            foods.Add(foodFromModel.Name,
                foodFromModel.ImageUrl,
                foodFromModel.Price,
                foodFromModel.Description,
                foodFromModel.CategoryId,
                itemId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Edit(int id)
        {
            var foodModel = foods.ShowFoodToEdit(id);

            return View(foodModel);
        }

        [Authorize(Roles = AdminConstants.Administrator)]
        [HttpPost]
        public IActionResult Edit(FoodFormModel editedModel, int id)
        {
            var itemId = foods.EditFood(
                id,
                editedModel.Name, 
                editedModel.ImageUrl,
                editedModel.Price,
                editedModel.Description,
                editedModel.CategoryId);

            items.Edit(
                itemId, 
                editedModel.Name,
                editedModel.Price);

            return RedirectToAction("Index", "Home");

        }
    }
}

