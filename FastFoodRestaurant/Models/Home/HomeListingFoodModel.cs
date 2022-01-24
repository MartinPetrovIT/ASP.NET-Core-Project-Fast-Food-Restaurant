using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Models.Home
{
    public class HomeListingFoodModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }


        public decimal Price { get; set; }

        public int ItemId { get; set; }

    }
}
