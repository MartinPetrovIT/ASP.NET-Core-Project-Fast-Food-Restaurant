using FastFoodRestaurant.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Test.Data
{
    public class Drinks
    {
        public static List<Drink> TenAlcoholicDrinks
         => Enumerable.Range(0, 10).Select(i => new Drink() {IsAlcoholic = true }).ToList();
        public static List<Drink> TenDrinks
       => Enumerable.Range(0, 10).Select(i => new Drink() ).ToList();
    }
}
