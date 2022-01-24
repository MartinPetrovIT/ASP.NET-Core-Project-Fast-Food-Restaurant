using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastFoodRestaurant.Data.DataConstants.Drink;
namespace FastFoodRestaurant.Models.Drink
{
    public class AddDrinkModel
    {
        [Required]
        [StringLength(maxDrinkNameLength, MinimumLength = minDrinkNameLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Range(minPrice,maxPrice)]
        public decimal Price { get; set; }

        public bool IsAlcoholic { get; init; }
    }
}
