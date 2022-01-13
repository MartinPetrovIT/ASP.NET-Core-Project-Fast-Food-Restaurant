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

        public IActionResult All([FromQuery]FoodSearchModel query)
        {
            var foodQuery = data.Foods.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                foodQuery = foodQuery.Where(f =>
                f.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                f.Description.ToLower().Contains(query.SearchTerm.ToLower()) ||
                f.Category.Name.ToLower().Contains(query.SearchTerm.ToLower()) 
                );
                
            }

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                foodQuery = foodQuery.Where(c => c.Category.Name == query.Category);

            }
            
            foodQuery = query.Sorting switch
            {
                FoodSorting.PriceAcsending => foodQuery.OrderBy(x => x.Price),
                FoodSorting.PriceDescending => foodQuery.OrderByDescending(x => x.Price),
                _ => foodQuery

            };

            var totalFood = foodQuery.Count();

            var foods = foodQuery
                .Skip((query.CurrentPage - 1) * FoodSearchModel.EntityPerPage)
                .Take(FoodSearchModel.EntityPerPage)
                .Select(x => new FoodListingModel
            {
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                Description = x.Description,
                Category = x.Category.Name

            }).ToList();

            var foodCategories = data.FoodCategories.Select(x => x.Name);


            query.Categories = foodCategories;
            query.Food = foods;
            query.TotalFood = totalFood;
            return View(query);
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
