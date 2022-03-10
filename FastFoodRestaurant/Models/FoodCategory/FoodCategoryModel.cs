using System.ComponentModel.DataAnnotations;

namespace FastFoodRestaurant.Models.FoodCategory
{
    public class FoodCategoryModel
    {

        public int Id { get; init; }

        [Required]
        public string Name { get; set; }
    }
}