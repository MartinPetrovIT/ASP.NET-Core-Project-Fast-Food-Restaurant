using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Data.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
