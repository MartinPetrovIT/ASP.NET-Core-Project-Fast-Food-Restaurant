using FastFoodRestaurant.Controllers;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FastFoodRestaurant.Test.Data.Drinks;
namespace FastFoodRestaurant.Test.Controllers
{
    public class DrinkControllerTest
    {
        [Fact]
        public void AllShouldReturnCorrectViewWithModel()
        {
            MyController<DrinkController>
                  .Instance(c => c.WithData(TenAlcoholicDrinks)
                  .WithData(TenDrinks))
                  .Calling(c => c.All("",false))
                  .ShouldReturn()
                  .View(v => v.WithModelOfType<Models.Drink.DrinkSearchModel>()
                  .Passing(m => m.Drink.Count() == 20));

            MyController<DrinkController>
                 .Instance(c => c.WithData(TenAlcoholicDrinks)
                 .WithData(TenDrinks))
                 .Calling(c => c.All("", true))
                 .ShouldReturn()
                 .View(v => v.WithModelOfType<Models.Drink.DrinkSearchModel>()
                 .Passing(m => m.Drink.Count() == 10));

        }


        [Fact]
        public void AllShouldReturnCorrectViewWithModelWithOnlyAlcoholFreeDrinks()
        {

            MyController<DrinkController>
                 .Instance(c => c.WithData(TenAlcoholicDrinks)
                 .WithData(TenDrinks))
                 .Calling(c => c.All("", true))
                 .ShouldReturn()
                 .View(v => v.WithModelOfType<Models.Drink.DrinkSearchModel>()
                 .Passing(m => m.Drink.Count() == 10));

        }
    }
}
