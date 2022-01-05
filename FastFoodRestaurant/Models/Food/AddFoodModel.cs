using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastFoodRestaurant.Data.DataConstants.Food;

namespace FastFoodRestaurant.Models.Food
{
    public class AddFoodModel
    {
        [Required]
        [Range(minFoodNameLength,maxFoodNameLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Range(minDescriptionLength, maxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [Range(minPrice,maxPrice)]
        public decimal Price { get; set; }

        public List<string> Ingredients { get; set; } = new List<string>();
    }
}
