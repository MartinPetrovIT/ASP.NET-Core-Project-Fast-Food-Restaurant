using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Data
{
    public class DataConstants
    {
        public class Food
        {
            public const int maxFoodNameLength = 30;
            public const int minFoodNameLength = 3;
            public const int maxDescriptionLength = 1000;
            public const int minDescriptionLength = 10;
            public const double maxPrice = 1000.00;
            public const double minPrice = 0.00;

        
        }
        public class Drink
        {
            public const int maxDrinkNameLength = 20;
            public const int minDrinkNameLength = 3;


        }
        public class Ingredient
        {
            public const int maxIngredientNameLength = 20;
            public const int minIngredientNameLength = 3;


        }
        public class Dessert
        {
            public const int maxDessertNameLength = 20;
            public const int minDessertNameLength = 3;
            public const double maxPrice = 1000.00;
            public const double minPrice = 0.00;


        }

        public class FoodCategory
        {
            public const int maxFoodCategoryNameLength = 20;
            public const int minFoodCategoryNameLength = 3;


        }
    }
}
