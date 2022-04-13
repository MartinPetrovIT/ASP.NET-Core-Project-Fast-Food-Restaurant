using FastFoodRestaurant.Areas.Admin.Controllers;
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
    public class FoodControllerAdminAreaTest
    {
        
        [Theory]
        [InlineData("Administrator")]
        public void GETAddShouldReturnVIew(string adminRole)
        {
            MyController<FoodController>
                  .Instance()
                  .WithUser(a => a.InRole(adminRole))
                  .WithData(TenFoodCategories)    
                  .Calling(c => c.Add())
                  .ShouldHave()
                  .ValidModelState()
                  .ActionAttributes(a => a.RestrictingForAuthorizedRequests(adminRole))
                  .AndAlso()
                  .ShouldReturn()
                  .View(v => v.WithModelOfType<Areas.Admin.Models.Food.FoodFormModel>()
                  .Passing(m => m.Categories.Count() == 10));
        }

        [Theory]
        [InlineData("Administrator")]
        public void POSTAddShouldReturnVIew(string adminRole)
        {
            MyController<FoodController>
                  .Instance()
                  .WithUser(a => a.InRole(adminRole))
                  .WithData(TenFoodCategories)
                  .Calling(c => c.Add(FoodFormModelWIthData))
                  .ShouldHave()
                  .ValidModelState()
                  .ActionAttributes(a => a.RestrictingForAuthorizedRequests(adminRole)
                  .RestrictingForHttpMethod(HttpMethod.Post)
                  .SettingRequestSizeLimitTo(10 * 1024 * 1024))
                  .AndAlso()
                  .ShouldReturn()
                  .Redirect("/");
        }

        [Theory]
        [InlineData("Administrator")]
        public void GETEditShouldReturnVIew(string adminRole)
        {
            MyController<FoodController>
                  .Instance()
                   .WithData(FoodWithData)
                   .WithData(TenFoodCategories)
                  .WithUser(a => a.InRole(adminRole))
                  .Calling(c => c.Edit(5))
                  .ShouldHave()
                  .ValidModelState()
                  .ActionAttributes(a => a.RestrictingForAuthorizedRequests(adminRole))
                  .AndAlso()
                  .ShouldReturn()
                  .View(v => v.WithModelOfType<Areas.Admin.Models.Food.FoodFormModel>()
                  .Passing(m => m.Name == FoodWithData.Name &&
                  m.Price == FoodWithData.Price &&
                  m.Description == FoodWithData.Description));
        }


        [Theory]
        [InlineData("Administrator")]
        public void POSTEditShouldReturnRedirect(string adminRole)
        {
            MyController<FoodController>
                  .Instance()
                  .WithUser(a => a.InRole(adminRole))
                  .WithData(TenFoodCategories)
                  .WithData(FoodWithData)
                  .WithData(Data.Items.Item)
                  .Calling(c => c.Edit(Data.Foods.FoodFormModelWithoutImage, 5))
                  .ShouldHave()
                  .ValidModelState()
                  .ActionAttributes(a => a.RestrictingForAuthorizedRequests(adminRole)
                  .RestrictingForHttpMethod(HttpMethod.Post)
                  .SettingRequestSizeLimitTo(10 * 1024 * 1024))
                  .AndAlso()
                  .ShouldReturn()
                  .Redirect("/");
        }


        [Theory]
        [InlineData("Administrator")]
        public void DeleteShouldReturnRedirect(string adminRole)
        {
            MyController<FoodController>
                  .Instance()
                  .WithUser(a => a.InRole(adminRole))
                  .WithData(TenFoodCategories)
                  .WithData(FoodWithData)
                  .WithData(Data.Items.Item)
                  .Calling(c => c.Delete(5))
                  .ShouldHave()
                  .ValidModelState()
                  .ActionAttributes(a => a.RestrictingForAuthorizedRequests(adminRole))
                  .AndAlso()
                  .ShouldReturn()
                  .Redirect("/");
        }

    }
}
