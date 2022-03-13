using FastFoodRestaurant.Models.Home;
using FastFoodResturant.Controllers;
using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FastFoodRestaurant.WebConstants.Cache;
using static FastFoodRestaurant.Test.Data.Foods;

namespace FastFoodRestaurant.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
        {
            MyController<HomeController>
                  .Instance(controller => controller
                      .WithData(TenFoods))
                  .Calling(c => c.Index())
                  .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey(LatestFoodsCacheKey)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                        .WithValueOfType<List<HomeListingFoodModel>>()))
                .AndAlso()
                  .ShouldReturn()
                  .View(view => view
                      .WithModelOfType<List<HomeListingFoodModel>>()
                      .Passing(model => model.Should().HaveCount(3)));
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            MyController<HomeController>
                 .Instance()
                 .Calling(c => c.Error())
                 .ShouldReturn()
                 .View();
        }
    }
}
