using FastFoodRestaurant.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Test.Data
{
    public class Items
    {
        public static Item Item
        => new ()
        {
            Id = 5,
            Name = "TestItem",
            Price = 1.00M
        };
}
}
