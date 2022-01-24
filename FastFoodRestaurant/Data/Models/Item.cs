using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Data.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public List<OrderItem> Orders = new();

        public Food Food { get; set; }

        public Drink Drink { get; set; }

    
    }
}
