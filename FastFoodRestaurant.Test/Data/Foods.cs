using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Food;
using FastFoodRestaurant.Services.Food;
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



        public static FoodSearchModel FoodSearchModel
        => new FoodSearchModel()
            {
                Categories = new string[] { foodServiceCategoryNameZ, foodServiceCategoryNameX},
                CurrentPage = 1,
                Food = TenFoodListingModels
        };


    }
}
