using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FastFoodRestaurant.Areas.Admin.Controllers;
using static FastFoodRestaurant.Test.Data.Drinks;
using FastFoodRestaurant.Areas.Admin.Models.Drink;
using FastFoodRestaurant.Services.Image;
using System.IO;

namespace FastFoodRestaurant.Test.Controllers
{
    public class DrinkControllerAdminAreaTest
    {

        [Theory]
        [InlineData("Administrator")]
        public void GETAddShouldReturnVIew(string adminRole)
        {
            MyController<DrinkController>
                  .Instance()
                  .WithUser(a => a.InRole(adminRole))
                  .Calling(c => c.Add())
                  .ShouldHave()
                  .ActionAttributes(a => a.RestrictingForAuthorizedRequests(adminRole))
                  .AndAlso()
                  .ShouldReturn()
                  .View();
        }

        [Theory]
        [InlineData("Administrator")]
        public void POSTAddShouldReturnVIew(string adminRole)
        {
            MyController<DrinkController>
                  .Instance()
                  .WithUser(a => a.InRole(adminRole))
                  .Calling(c => c.Add(Data.Drinks.DrinkFormModel))
                  .ShouldHave()
                  .ValidModelState()
                  .ActionAttributes(a => a.RestrictingForAuthorizedRequests(adminRole)
                  .RestrictingForHttpMethod(HttpMethod.Post)
                  .SettingRequestSizeLimitTo(10 * 1024 * 1024))
                  .AndAlso()
                  .ShouldReturn()
                  .Redirect("/");

            File.Delete($"{WebConstants.Image.UploadDirectory}/{Data.Drinks.DrinkFormModel.Image.FileName}");




        }

        [Theory]
        [InlineData("Administrator")]
        public void GETEditShouldReturnVIew(string adminRole)
        {
            MyController<DrinkController>
                  .Instance()
                   .WithData(drink)
                  .WithUser(a => a.InRole(adminRole))
                  .Calling(c => c.Edit(1))
                  .ShouldHave()
                  .ValidModelState()
                  .ActionAttributes(a => a.RestrictingForAuthorizedRequests(adminRole))
                  .AndAlso()
                  .ShouldReturn()
                  .View(v => v.WithModelOfType<DrinkFormModel>()
                  .Passing(m => m.IsAlcoholic == drink.IsAlcoholic &&
                  m.Price == drink.Price &&
                  m.Name == drink.Name));
        }


        [Theory]
        [InlineData("Administrator")]
        public void POSTEditShouldReturnRedirect(string adminRole)
        {
            MyController<DrinkController>
                  .Instance()
                  .WithUser(a => a.InRole(adminRole))
                  .WithData(drink)
                  .WithData(Data.Items.Item)
                  .Calling(c => c.Edit(Data.Drinks.DrinkFormModelWithoutImage , 1))
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
            MyController<DrinkController>
                  .Instance()
                  .WithUser(a => a.InRole(adminRole))
                  .WithData(drinkWithValidFileName)
                  .WithData(Data.Items.Item)
                  .Calling(c => c.Delete(1))
                  .ShouldHave()
                  .ValidModelState()
                  .ActionAttributes(a => a.RestrictingForAuthorizedRequests(adminRole))
                  .AndAlso()
                  .ShouldReturn()
                  .Redirect("/");
        }
    }
}
