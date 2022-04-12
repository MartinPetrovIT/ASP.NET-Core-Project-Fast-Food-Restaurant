using FastFoodRestaurant.Controllers;
using FastFoodRestaurant.Data.Models;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FastFoodRestaurant.Test.Data.Client;
using static FastFoodRestaurant.Test.Data.Items;
using static FastFoodRestaurant.Test.Data.Orders;
using static FastFoodRestaurant.WebConstants;
namespace FastFoodRestaurant.Test.Controllers
{
    public class OrderControllerTest
    {
        [Fact]
        public void OrderNowShouldReturnRedirectToActionIndexHome()
        {
            MyController<OrderController>
               .Instance(c => c.WithUser()
               .WithData(ValidTestClient)
               .WithData(Data.Items.Item))
               .Calling(c => c.OrderNow(Data.Items.Item.Id))
               .ShouldHave()
               .ValidModelState()
               .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldHave()
               .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
               .TempData(x => x.ContainingEntryWithKey(GlobalMessageKey)
               .AndAlso()
               .ContainingEntryWithValue(ItemIsAdded))
               .AndAlso()
               .ShouldReturn()
               .RedirectToAction("Index", "Home");
        
        }

        [Fact]
        public void CartShouldReturnCorrectViewWithModel()
        {
            MyController<OrderController>
                   .Instance(c => c.WithUser()
                   .WithData(ValidTestClient)
                   .WithData(new Order() { ClientId = ValidTestClient.Id}))
            .Calling(c => c.Cart(OrderListingModel))
            .ShouldHave()
            .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .View(v => v.WithModelOfType<Models.Order.OrderListingModel>()
            .Passing(m => 
            m.InformationModel.Name != OrderListingModel.InformationModel.Name &&
            m.InformationModel.Address != OrderListingModel.InformationModel.Address &&
            m.InformationModel.PhoneNumber != OrderListingModel.InformationModel.PhoneNumber 
            ));

        }


        [Fact]
        public void PlusQuantityShouldReturnRedirect()
        {
            MyController<OrderController>
                   .Instance(c => c.WithUser()
                   .WithData(ValidTestClient)
                   .WithData(Data.Orders.OrderItem))
                   .Calling(c => c.PlusQuantity(Data.Orders.OrderItem.ItemId,
                   Data.Orders.OrderItem.OrderId))
                   .ShouldHave()
                   .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
                   .NoTempData()
                   .AndAlso()
                   .ShouldReturn()
                   .RedirectToAction("Cart", "Order");
   

        }

        [Fact]
        public void MinusQuantityShouldReturnRedirect()
        {
            MyController<OrderController>
                   .Instance(c => c.WithUser()
                   .WithData(ValidTestClient)
                   .WithData(Data.Orders.OrderItem))
                   .Calling(c => c.MinusQuantity(Data.Orders.OrderItem.ItemId,
                   Data.Orders.OrderItem.OrderId))
                   .ShouldHave()
                   .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
                   .NoTempData()
                   .AndAlso()
                   .ShouldReturn()
                   .RedirectToAction("Cart", "Order");


        }

        [Fact]
        public void RemoveShouldReturnRedirect()
        {
            MyController<OrderController>
                   .Instance(c => c.WithUser()
                   .WithData(ValidTestClient)
                   .WithData(Data.Orders.OrderItem))
                   .Calling(c => c.Remove(Data.Orders.OrderItem.ItemId,
                   Data.Orders.OrderItem.OrderId))
                   .ShouldHave()
                   .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
                   .AndAlso()
                   .ShouldReturn()
                   .RedirectToAction("Cart", "Order");


        }

        [Fact]
        public void CompleteOrderShouldReturnRedirect()
        {
            MyController<OrderController>
                   .Instance(c => c.WithUser()
                   .WithData(ValidTestClient)
                   .WithData(Data.Orders.Order))
                   .Calling(c => c.CompleteOrder(Data.Orders.Order.Id,
                   10))
                   .ShouldHave()
                   .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
                   .TempData(d => d.ContainingEntryWithKey(WebConstants.GlobalMessageKey)
                   .ContainingEntryWithValue(WebConstants.CompletedOrder))
                   .AndAlso()
                   .ShouldReturn()
                   .RedirectToAction("Index", "Home");


        }

        [Theory]
        [InlineData("13/04/2022", "04/13/2022")]
        public void MyOrderHistoryShouldReturnRedirect(string date, string date2)
        {
            MyController<OrderController>
                   .Instance(c => c.WithUser()
                   .WithData(ValidTestClient)
                   .WithData(Data.Orders.OrderWithInlineDate(date2)))
                   .Calling(c => c.MyOrderHistory(date))
                   .ShouldHave()
                   .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
                   .NoTempData()
                   .AndAlso()
                   .ShouldReturn()
                   .View(v => v.WithModelOfType<List<Models.Order.OrderHistoryModel>>()
                   .Passing(m => m.Count == 1));


        }


    }
}
