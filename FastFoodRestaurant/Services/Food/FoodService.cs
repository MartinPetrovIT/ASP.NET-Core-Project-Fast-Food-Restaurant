using FastFoodRestaurant.Models.Food;
using FastFoodResturant.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFoodRestaurant.Models.FoodCategory;
using FastFoodRestaurant.Services.Food;
using FastFoodRestaurant.Services.FoodCategory;
using FastFoodRestaurant.Areas.Admin.Models;
using FastFoodRestaurant.Areas.Admin.Models.Food;
using FastFoodRestaurant.Models.Home;

namespace FastFoodRestaurant.Services.Food
{
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext data;

        public FoodService(ApplicationDbContext data)
        {
            this.data = data;
        }
        
        public FoodSearchModel All(
            string searchTerm,
            string category,
            FoodSorting foodSorting,
            int currentPage ,
            int entityPerPage)
        {

            var foodQuery = data.Foods.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                foodQuery = foodQuery.Where(f =>
                f.Name.ToLower().Contains(searchTerm.ToLower()) ||
                f.Description.ToLower().Contains(searchTerm.ToLower()) ||
                f.Category.Name.ToLower().Contains(searchTerm.ToLower())
                );

            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                foodQuery = foodQuery.Where(c => c.Category.Name == category);

            }

            foodQuery = foodSorting switch
            {
                FoodSorting.PriceAcsending => foodQuery.OrderBy(x => x.Price),
                FoodSorting.PriceDescending => foodQuery.OrderByDescending(x => x.Price),
                _ => foodQuery

            };

            var totalFood = foodQuery.Count();

            var foods = foodQuery
                .Skip((currentPage - 1) * FoodSearchModel.EntityPerPage)
                .Take(FoodSearchModel.EntityPerPage)
                .Select(x => new FoodServiceListingModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price,
                    Description = x.Description,
                    Category = x.Category.Name,
                    ItemId = x.ItemId

                }).ToList();


            var foodCategories = data.FoodCategories.Select(x => x.Name);

          
            return new FoodSearchModel
            {
                Food = foods,
                TotalFood = totalFood,
                CurrentPage = currentPage,
                Categories = foodCategories

            };
        }

        void IFoodService.Add(
            string name,
            string imageUrl,
            decimal price,
            string description,
            int categoryId,
            int itemId)
        {
            var food = new FastFoodRestaurant.Data.Models.Food()
            {
                Name = name,
                ImageUrl = imageUrl,
                Price = price,
                Description = description,
                CategoryId = categoryId,
                ItemId = itemId

            };

            data.Foods.Add(food);
            data.SaveChanges();
        }

        public FoodFormModel ShowFoodToEdit(int foodId)
        {
            var listOfCategories = GetFoodCategories();
            var foodModel = data.Foods.Where(x => x.Id == foodId).Select(x => new FoodFormModel
            {
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                Description = x.Description,
                CategoryId = x.CategoryId,
                Categories = listOfCategories

            }).FirstOrDefault();


            return foodModel;
        
        }

        public int EditFood(int foodId, string name, string imageUrl, decimal price, string description, int categoryId)
        {
            var listOfCategories = GetFoodCategories();
            var foodModel = data.Foods.Where(x => x.Id == foodId).FirstOrDefault();

            foodModel.Name = name;
            foodModel.ImageUrl = imageUrl;
            foodModel.Price = price;
            foodModel.Description = description;
            foodModel.CategoryId = categoryId;

            data.SaveChanges();

            return foodModel.ItemId;

        }


        public List<HomeListingFoodModel> TakeLastAddedFoods()
        {
            var foods = data.Foods.OrderByDescending(x => x.Id).Select(x => new HomeListingFoodModel
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                ItemId = x.ItemId
            }).Take(3).ToList();

            return foods;
        }
        public List<Models.FoodCategory.FoodCategoryModel> GetFoodCategories()
        {
            List<Models.FoodCategory.FoodCategoryModel> categories =
                  data.FoodCategories.Select(x => new Models.FoodCategory.FoodCategoryModel
                  {
                      Id = x.Id,
                      Name = x.Name

                  }).ToList();

            return categories;
        }

       
    }
}
