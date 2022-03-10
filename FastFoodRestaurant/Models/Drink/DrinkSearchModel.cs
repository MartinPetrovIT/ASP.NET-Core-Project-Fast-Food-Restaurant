using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFoodRestaurant.Models.Drink
{
    public class DrinkSearchModel
    {
  

        public IEnumerable<DrinkListingModel> Drink { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        [Display(Name = "Alcohol free only")]
        public bool AlcoholFreeOnly { get; set; }

}
}
