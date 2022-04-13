using FastFoodRestaurant.Controllers;
using FastFoodRestaurant.Models.Client;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FastFoodRestaurant.Test.Data.TestDataConstants.Client;
using static FastFoodRestaurant.Test.Data.Client;
namespace FastFoodRestaurant.Test.Controllers
{
    public class ClientControllerTest
    {
        [Fact]
        public void GETInformationShouldReturnNotFoundIfUserNotLoggedIn()
        {

            MyController<ClientController>
                 .Instance()
                 .Calling(c => c.Information())
                 .ShouldReturn()
                 .NotFound();

        }

        [Fact]
        public void GETInformationShouldReturnCorrectModelWithViewAndData()
        {

            MyController<ClientController>
                 .Instance(c => c.WithUser()
                 .WithData(ValidTestClient))
                 .Calling(c => c.Information())
                 .ShouldHave()
                 .ValidModelState()
                 .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
                 .AndAlso()
                 .ShouldReturn()
                 .View(v => v.WithModelOfType<InformationModel>()
                .Passing(m => m.PhoneNumber == TestPhoneNumber &&
                m.Address == TestAddress &&
                m.Name == TestName));

        }


        [Theory]
        [InlineData("Ivan","Besarabia 122", "0888858298")]
        public void PostInformationShouldReturnCorrectModelWithView(string name,string address,string phone)
        {

            MyController<ClientController>
                 .Instance(c => c.WithUser()
                 .WithData(ValidTestClient))
                 .Calling(c => c.Information(new InformationModel()
                 {
                     Name = name,
                     PhoneNumber = phone,
                     Address = address
                 }))
                 .ShouldHave()
                 .ValidModelState()
                 .ActionAttributes(a => a.RestrictingForAuthorizedRequests()
                 .RestrictingForHttpMethod(HttpMethod.Post))
                 .AndAlso()
                 .ShouldReturn()
                 .RedirectToAction("Index", "Home");
        }
    }
}
