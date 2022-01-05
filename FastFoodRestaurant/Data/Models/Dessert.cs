using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastFoodRestaurant.Data.DataConstants.Dessert;

namespace FastFoodRestaurant.Data.Models
{
    public class Dessert
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(maxDessertNameLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }


        public decimal Price { get; set; }
    }
}
