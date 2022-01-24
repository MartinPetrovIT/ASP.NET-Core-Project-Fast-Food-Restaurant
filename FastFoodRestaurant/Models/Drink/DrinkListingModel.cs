using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Models.Drink
{
    public class DrinkListingModel
    {
        public string Name { get; set; }

        
        public string ImageUrl { get; set; }

        
        public decimal Price { get; set; }

        public bool IsAlcoholic { get; init; }
    }
}
