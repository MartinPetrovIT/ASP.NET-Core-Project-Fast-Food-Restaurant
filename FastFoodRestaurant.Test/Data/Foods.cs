using FastFoodRestaurant.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Test.Data
{
    public class Foods
    {
        public static List<Food> TenFoods
           => Enumerable.Range(0, 10).Select(i => new Food()).ToList();
    }
}
