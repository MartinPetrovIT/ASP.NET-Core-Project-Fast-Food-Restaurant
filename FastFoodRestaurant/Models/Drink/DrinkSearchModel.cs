using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Models.Drink
{
    public class DrinkSearchModel
    {
  

        public IEnumerable<DrinkListingModel> Drink { get; init; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        [Display(Name = "Alcohol free only")]
        public bool AlcoholFreeOnly { get; init; }

}
}
