using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Services.FoodCategory
{
    public class FoodCategoryModel
    {

      public int Id { get; init; }
    
      [Required]
      public string Name { get; set; }
    }
}
