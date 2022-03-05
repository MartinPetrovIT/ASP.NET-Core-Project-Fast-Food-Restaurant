using FastFoodRestaurant.Models.Client;
using FastFoodRestaurant.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Models.Order
{
    public class OrderListingModel
    {

        public int Id { get; set; }

        public List<ItemListingModel> Items = new();

        public decimal TotalSum { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime OrderDate { get; set; }

        public InformationModel InformationModel { get; set; }

    }
}
