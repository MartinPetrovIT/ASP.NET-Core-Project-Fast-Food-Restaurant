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

        public FoodController(IFoodService foods, IItemService items)
        {
            this.foods = foods;
            this.items = items;
        }

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
          
            if (!ModelState.IsValid)
            {
                foodFromModel.Categories = foods.GetFoodCategories();
                return View(foodFromModel);
            }

            if (!foods.CheckCategoryId(foodFromModel.CategoryId))
            {
                ModelState.AddModelError(nameof(foodFromModel), "Category does not exist!");
            }

            var itemId = items.Add(foodFromModel.Name, foodFromModel.Price);

               foods.Add(foodFromModel.Name,
               foodFromModel.ImageUrl,
               foodFromModel.Price,
               foodFromModel.Description,
               foodFromModel.CategoryId,
               itemId);

            


            return Redirect("/");
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
            if (!ModelState.IsValid)
            {
                editedModel.Categories = foods.GetFoodCategories();
                return View(editedModel);
            }

            if (!foods.CheckCategoryId(editedModel.CategoryId))
            {
                ModelState.AddModelError(nameof(editedModel), "Category does not exist!");
            }

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

            return Redirect("/");

        }

        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Delete(int id)
        {
            items.Delete(foods.Delete(id));

            return Redirect("/");
        }
    }
}

