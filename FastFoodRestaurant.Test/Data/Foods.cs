using FastFoodRestaurant.Areas.Admin.Models.Food;
using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Food;
using FastFoodRestaurant.Models.FoodCategory;
using FastFoodRestaurant.Services.Food;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Test.Data
{
    public class Foods
    {
        private const string foodServiceCategoryNameX = "Pasta";
        private const string foodServiceCategoryNameZ = "Pizza";
        public static List<Food> TenFoods
           => Enumerable.Range(0, 10).Select(i => new Food()).ToList();

        public static List<FoodServiceListingModel> TenFoodListingModels
         => Enumerable.Range(0, 10).Select(i => new FoodServiceListingModel()).ToList();

        public static Food FoodWithData(int id)
        => new()
        {
            Id = id,
            Name = "Test Food",
            Price = 1.00M,
            Description = "Test Food Description",
            ImageUrl = "http://www.sulitest.org/files/source/logo%20test%20horiz%20bis.png?1574185700651",
            ItemId = id,


        };


        public static FoodSearchModel FoodSearchModel
        => new FoodSearchModel()
        {
            Categories = new string[] { foodServiceCategoryNameZ, foodServiceCategoryNameX },
            CurrentPage = 1,
            Food = TenFoodListingModels
        };

        public static List<FoodCategoryModel> FoodCategoryModelList
     => Enumerable.Range(0, 10).Select(x => new FoodCategoryModel()).ToList();


        public static FoodFormModel FoodFormModel(string name, decimal price, string description)
     => new()
     {
         Name = name,
         Description = description,
         Price = price,
         CategoryId = 1,
         ImageUrl = "http://www.sulitest.org/files/source/logo%20test%20horiz%20bis.png?1574185700651",
         //Categories = FoodCategoryModelList
     };

        public static FoodFormModel FoodFormModel(string name, decimal price, string description, int categoryId)
    => new()
    {
        Name = name,
        Description = description,
        Price = price,
        CategoryId = categoryId,
        ImageUrl = "http://www.sulitest.org/files/source/logo%20test%20horiz%20bis.png?1574185700651",
         //Categories = FoodCategoryModelList
     };

        public static List<FoodCategory> FoodCategories
  => Enumerable.Range(0, 1).Select(x => new FoodCategory() { Id = 1 }).ToList();
}
}
