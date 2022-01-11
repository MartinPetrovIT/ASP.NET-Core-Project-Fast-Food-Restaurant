using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Food;
using FastFoodRestaurant.Models.FoodCategory;
using FastFoodResturant.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Controllers
{
    public class FoodController : Controller
    {
        public FoodController(ApplicationDbContext data)
        {
        
            this.data = data;
        }
        private readonly ApplicationDbContext data;

        public IActionResult Add()
        {
            
            return View(new AddFoodModel {  Categories = this.GetFoodCategories()});
        }

        [HttpPost]
        public IActionResult Add(AddFoodModel foodFromModel)
        {
            if (!this.data.FoodCategories.Any(c => c.Id == foodFromModel.CategoryId))
            {
                ModelState.AddModelError(nameof(foodFromModel), "Category does not exist!");
            }
            if (!ModelState.IsValid)
            {
                foodFromModel.Categories = this.GetFoodCategories();
                return View(foodFromModel);
            }

            var food = new Food()
            {
                Name = foodFromModel.Name,
                ImageUrl = foodFromModel.ImageUrl,
                Price = foodFromModel.Price,
                Description = foodFromModel.Description,
                CategoryId = foodFromModel.CategoryId

            };

            data.Foods.Add(food);

            data.SaveChanges();

            return RedirectToAction("Index" , "Home");
        }

        public IActionResult All()
        {
            var foods = data.Foods.Select(x => new FoodListingModel
            {
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                Description = x.Description,
                Category = x.Category.Name

            }).ToList();

            return View(foods);
        }

        private IEnumerable<FoodCategoryModel> GetFoodCategories()
        {
          List<FoodCategoryModel> categories =  
                data.FoodCategories.Select(x => new FoodCategoryModel
                { 
                 Id = x.Id,
                 Name = x.Name
                
                }).ToList();

            return categories;
        }






    }
}
