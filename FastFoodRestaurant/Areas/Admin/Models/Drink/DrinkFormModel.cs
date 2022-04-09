using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastFoodRestaurant.Data.DataConstants.Drink;
namespace FastFoodRestaurant.Areas.Admin.Models.Drink
{
    public class DrinkFormModel
    {
        [Required]
        [StringLength(maxDrinkNameLength, MinimumLength = minDrinkNameLength)]
        public string Name { get; set; }

        [Range(minPrice,maxPrice)]
        public decimal Price { get; set; }

        
        public IFormFile Image{ get; set; }

        public bool IsAlcoholic { get; init; }
    }
}
