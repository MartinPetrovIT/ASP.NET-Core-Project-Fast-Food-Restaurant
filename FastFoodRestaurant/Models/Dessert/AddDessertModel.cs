using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastFoodRestaurant.Data.DataConstants.Dessert;

namespace FastFoodRestaurant.Models.Dessert
{
    public class AddDessertModel
    {

        [Required]
        [Range(minDessertNameLength,maxDessertNameLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Range(minPrice, maxPrice)]
        public decimal Price { get; set; }
    }
}
