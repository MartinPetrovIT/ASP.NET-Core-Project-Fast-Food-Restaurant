using FastFoodRestaurant.Controllers;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FastFoodRestaurant.Test.Routing
{
    public class FoodControllerTest
    {
        [Fact]
        public void AllMapShouldBeMapped()
        => MyRouting
                .Configuration()
                .ShouldMap("/Food/All")
                .To<FoodController>(c => c.All(new Models.Food.FoodSearchModel()));
    }
}
