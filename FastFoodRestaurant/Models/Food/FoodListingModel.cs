using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Models.Food
{
    public class FoodListingModel
    {

        public string Name { get; set; }

        public string ImageUrl { get; set; }


        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

    }
}
