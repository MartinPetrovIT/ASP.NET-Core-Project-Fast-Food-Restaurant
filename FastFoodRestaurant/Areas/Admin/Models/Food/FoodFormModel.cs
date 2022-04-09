using FastFoodRestaurant.Models.FoodCategory;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static FastFoodRestaurant.Data.DataConstants.Food;

namespace FastFoodRestaurant.Areas.Admin.Models.Food
{
    public class FoodFormModel
    {
        [Required]
        [StringLength(maxFoodNameLength, MinimumLength = minFoodNameLength)]
        public string Name { get; set; }
        public IFormFile Image { get; set; }

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
