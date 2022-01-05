using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastFoodRestaurant.Data.DataConstants.FoodCategory;

namespace FastFoodRestaurant.Data.Models
{
    public class FoodCategory
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(maxFoodCategoryNameLength)]
        public string Name { get; set; }

        public List<Food> Foods { get; set; } = new List<Food>();
    }
}
