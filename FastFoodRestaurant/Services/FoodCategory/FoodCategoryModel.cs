using System.ComponentModel.DataAnnotations;

namespace FastFoodRestaurant.Services.FoodCategory
{
    public class FoodCategoryModel
    {

      public int Id { get; init; }
    
      [Required]
      public string Name { get; set; }
    }
}
