using FastFoodRestaurant.Areas.Admin.Models.Drink;
using FastFoodRestaurant.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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


        public static DrinkFormModel DrinkFormModel = new()
        {
            Image = Images.Image(),
            Name = "ValidDrink",
            Price = 1.00M,
            IsAlcoholic = false
        };

        public static DrinkFormModel DrinkFormModelWithoutImage = new()
        {
            Name = "ValidDrink",
            Price = 1.00M,
            IsAlcoholic = false
        };
        public static Drink drink = new Drink()
        {
            Id = 1,
            ImageFileName = "TestFileName",
            IsAlcoholic = false,
            Name = "TestDrink",
            Price = 1.00M,
            ItemId = 5
        };

        public static Drink drinkWithValidFileName = new Drink()
        {
            Id = 1,
            ImageFileName = Images.Image().FileName,
            IsAlcoholic = false,
            Name = "TestDrink",
            Price = 1.00M,
            ItemId = 5
        };







    }
}
