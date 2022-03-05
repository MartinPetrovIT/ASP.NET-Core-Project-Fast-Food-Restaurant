using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Food;
using FastFoodRestaurant.Models.Home;
using FastFoodRestaurant.Models.Item;
using FastFoodRestaurant.Models.Order;
using FastFoodRestaurant.Services.Client;
using FastFoodRestaurant.Services.Order;
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
        public OrderController(IClientService clientService, IOrderService orderService)
        {
            this.clientService = clientService;
      
            this.orderService = orderService;
        }
        private readonly IClientService clientService;
        private readonly IOrderService orderService;
       


      

     
        [Authorize]
        public  IActionResult OrderNow(int itemId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            orderService.OrderNow(userId, itemId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Cart(OrderListingModel orderModel)
        {
           
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var flag = orderService.Cart(userId, orderModel);

            if (flag == null)
            {
                return RedirectToAction("Cart", "Order");
            }
            if (flag == true)
            {
              orderModel.InformationModel = clientService.ShowInformation(userId);
            }

            return View(orderModel);
        }

        public IActionResult ChangeInformation()
        {

            return RedirectToAction("Information", "Client");
        }

        public IActionResult PlusQuantity(int itemId, int orderId)
        {
            var oi = orderService.PlusQuantity(itemId, orderId);
            if (oi == null)
            {
                return NotFound();
            }

            return RedirectToAction("Cart", "Order");
        }
        public IActionResult MinusQuantity(int itemId, int orderId)
        {
            var oi = orderService.MinusQuantity(itemId, orderId);
            if (oi == null)
            {
                return NotFound();
            }
                
            if (oi == false)
            {
                return RedirectToAction("Cart", "Order");
                //TODO: Some message to click remove button
            }

            return RedirectToAction("Cart", "Order");
        }
        public IActionResult Remove(int itemId, int orderId)
        {
            var oi = orderService.Remove(itemId, orderId);
            if (oi == null)
            {
                return NotFound();
            }

            return RedirectToAction("Cart", "Order");
        }

        public IActionResult MyOrderHistory()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var allOrders = orderService.MyOrderHistory(userId);

            return View(allOrders);
        }

        public IActionResult CompleteOrder(int orderId, decimal totalSum)
        {
            var flag = orderService.CompleteOrder(orderId, totalSum);

           
           
            if (flag == null)
            {
                return NotFound();
                //TODO: make something better
            }
           
          

           

            if (flag == false)
            {
               return RedirectToAction("Cart", "Order");
               
            }

            
            return RedirectToAction("Index", "Home");
        }
    }
}
