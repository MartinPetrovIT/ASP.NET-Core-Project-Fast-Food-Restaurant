using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Data.Models
{
    public class FoodIngredient
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient{ get; set; }
    }
}
