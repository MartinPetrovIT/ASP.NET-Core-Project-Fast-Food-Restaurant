using FastFoodRestaurant.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastFoodRestaurant.Data.DataConstants.Ingredient;

namespace FastFoodRestaurant.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(maxIngredientNameLength)]
        public string Name { get; set; }

        public bool IsSpicy { get; set; }

        public List<FoodIngredient> FoodIngredients { get; set; } = new List<FoodIngredient>();
    }
}
