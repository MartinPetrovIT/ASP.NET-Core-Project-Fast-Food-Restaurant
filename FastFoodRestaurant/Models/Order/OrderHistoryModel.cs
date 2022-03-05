using FastFoodRestaurant.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Models.Order
{
    public class OrderHistoryModel
    {
        public DateTime OrderDate { get; set; }


        public decimal FullPrice { get; set; }

        public List<ItemListingModel> Items = new List<ItemListingModel>();
    }
}
