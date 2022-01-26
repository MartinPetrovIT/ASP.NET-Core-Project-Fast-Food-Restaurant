using FastFoodRestaurant.Services.Food;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Models.Food
{
    public class FoodSearchModel
    {
        public const int EntityPerPage = 4;

        public string Category { get; init; }

        public IEnumerable<FoodServiceListingModel> Food { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalFood { get; set; }

        [Display(Name ="Search")]
        public string SearchTerm { get; init; }

        public FoodSorting Sorting { get; set; } 
    }
}
