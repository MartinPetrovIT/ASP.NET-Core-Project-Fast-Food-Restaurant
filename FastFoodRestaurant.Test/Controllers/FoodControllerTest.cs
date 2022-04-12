using FastFoodRestaurant.Controllers;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FastFoodRestaurant.Test.Data.Foods;

namespace FastFoodRestaurant.Test.Controllers
{
    public class FoodControllerTest
    {

        [Fact]
        public void AllShouldReturnCorrectViewWithModel()
        {
            MyController<FoodController>
                  .Instance(c => c.WithData(TenFoods))
                  .Calling(c => c.All(FoodSearchModel))
                  .ShouldReturn()
                  .View(v => v.WithModelOfType<Models.Food.FoodSearchModel>()
                  .Passing(m => m.CurrentPage == 1 && m.TotalFood == 10));
                  
                  
        }
    }
}
