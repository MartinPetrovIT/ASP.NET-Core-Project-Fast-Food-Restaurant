using FastFoodRestaurant.Models.Client;
using FastFoodRestaurant.Models.Item;
using System;
using System.Collections.Generic;

namespace FastFoodRestaurant.Models.Order
{
    public class OrderHistoryModel
    {
        public DateTime OrderDate { get; set; }

        public InformationModel ClientInfoModel { get; set; } = new ();

        public decimal FullPrice { get; set; }

        public List<ItemListingModel> Items = new ();
        

    }
}
