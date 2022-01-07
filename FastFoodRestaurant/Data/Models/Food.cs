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
 
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
     
        public FoodCategory Category { get; set; }

    }
}
