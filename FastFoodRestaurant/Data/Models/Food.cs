using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastFoodRestaurant.Data.DataConstants.Food;

namespace FastFoodRestaurant.Data.Models
{
    public class Food
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(maxFoodNameLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]  
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
     
        public FoodCategory Category { get; set; }

     
        public  List<FoodIngredient> FoodIngredients { get; set; } = new List<FoodIngredient>();

       
        public int SpicyLevel => this.FoodIngredients.Select(x => x.Ingredient.IsSpicy == true).Count();
        //int spacy level
        //category

    }
}
