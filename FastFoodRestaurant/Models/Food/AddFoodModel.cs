using FastFoodRestaurant.Models.FoodCategory;
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
        [StringLength(maxFoodNameLength, MinimumLength = minFoodNameLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }


        [Required]
        [Range(minPrice,maxPrice)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(maxDescriptionLength, MinimumLength = minDescriptionLength)]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<FoodCategoryModel> Categories { get; set; }


    }
}
