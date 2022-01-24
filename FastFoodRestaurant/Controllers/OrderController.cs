using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Food;
using FastFoodRestaurant.Models.Home;
using FastFoodRestaurant.Models.Item;
using FastFoodRestaurant.Models.Order;
using FastFoodResturant.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Controllers
{
    public class OrderController : Controller
    {
        public OrderController(ApplicationDbContext data)
        {

            this.data = data;
        }
        private readonly ApplicationDbContext data;

        [Authorize]
        public IActionResult OrderNow() 
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public  IActionResult OrderNow(int itemId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var item = data.Items.Where(x => x.Id == itemId).FirstOrDefault();

            var clientOrders = data.Orders.Where(x => x.Client.Id == userId);
            if (item == null)
            {
                throw new ArgumentException();
            }

            if (userId == null)
            {
                throw new ArgumentException();
            }
            if (clientOrders.Count() == 0)
            {
                Order order = new Order()
                {
                    ClientId = userId,
                    
                };

                OrderItem orderItem = new OrderItem
                {
                    ItemId = item.Id,
                    OrderId = order.Id,
                    Order = order
     
                };

                data.OrderItems.Add(orderItem);
                data.Orders.Add(order);
                data.SaveChanges();
            }
            var clientOrder = clientOrders.Where(x => x.IsCompleted == false).FirstOrDefault();

            if (clientOrder == null)
            {
                Order order = new Order()
                {
                    ClientId = userId,

                };

                OrderItem orderItem = new OrderItem
                {
                    ItemId = item.Id,
                    OrderId = order.Id,
                    Order = order

                };

                data.OrderItems.Add(orderItem);
                data.Orders.Add(order);
                data.SaveChanges();

            }
            else
            {
                var orderItema = data.OrderItems
                    .Where(x => x.ItemId == itemId && x.OrderId == clientOrder.Id)
                    .FirstOrDefault();

                if (orderItema != null)
                {
                    orderItema.Quantity++;
                }
                else
                {

                OrderItem orderItem = new OrderItem
                {
                    ItemId = item.Id,
                    OrderId = clientOrder.Id,
                    Order = clientOrder

                };

                data.OrderItems.Add(orderItem);
                }

            }

            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Cart(OrderListingModel orderModel)
        {
           
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var clientOrders = data.Orders.Where(x => x.Client.Id == userId).ToList();

            var clientOrder = clientOrders.Where(x => x.IsCompleted == false).FirstOrDefault();

            var itemIds = data.OrderItems.Where(x => x.OrderId == clientOrder.Id)
                .Select(x => new { x.ItemId, x.Quantity })
                .ToList();

           

            foreach (var itemIdAndQuantity in itemIds)
            {
                
                if (!(data.Items.Any(x => x.Id == itemIdAndQuantity.ItemId)) )
                {
                    throw new ArgumentException();
                }
                var item = data.Items.Where(x => x.Id == itemIdAndQuantity.ItemId).FirstOrDefault();

                ItemListingModel itemListing = new ItemListingModel
                { 
                    Id = itemIdAndQuantity.ItemId,
                    Name = item.Name,
                    Price  = item.Price,
                    Quantity = itemIdAndQuantity.Quantity
                    
                };


                orderModel.Items.Add(itemListing);
            }

            orderModel.TotalSum = orderModel.Items.Sum(x => x.Price * x.Quantity);
            orderModel.Id = clientOrder.Id;
            return View(orderModel);
        }

       

    }
}
