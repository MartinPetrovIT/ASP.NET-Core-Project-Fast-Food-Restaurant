using FastFoodRestaurant.Models.Order;
using FastFoodRestaurant.Services.Client;
using FastFoodRestaurant.Services.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace FastFoodRestaurant.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        public OrderController(IClientService clientService, IOrderService orderService)
        {
            this.clientService = clientService;
      
            this.orderService = orderService;
        }
        private readonly IClientService clientService;
        private readonly IOrderService orderService;
       


      

     

        public  IActionResult OrderNow(int itemId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            orderService.OrderNow(userId, itemId);

            TempData[WebConstants.GlobalMessageKey] = "The item is added to your cart!";

            return RedirectToAction("Index", "Home");
        }

     
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


        //public IActionResult MyOrderHistory()
        //{
        //    var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        //    var allOrders = orderService.MyOrderHistory(userId);

        //    return View(allOrders);
        //}

       
        public IActionResult MyOrderHistory(string dDate)
        {
            if (dDate is null)
            {
                dDate = DateTime.UtcNow.ToString("dd/MM/yyyy");
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var allOrders = orderService.MyOrderHistory(userId);

            var filteredOrders = orderService.FilterDate(allOrders, dDate);

            return View(filteredOrders);
        }
        public IActionResult CompleteOrder(int orderId, decimal totalSum)
        {
            
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var flag = orderService.CompleteOrder(orderId, totalSum, userId);

           
           
            if (flag == null)
            {
                return NotFound();
            }
           

            if (flag == false)
            {
                TempData[WebConstants.GlobalWarningMessageKey] = "Order price can not be 0.00 and all information fields must be filled!";

                return RedirectToAction("Cart", "Order");  
            }

            TempData[WebConstants.GlobalMessageKey] = "Your order is completed!";

            return RedirectToAction("Index", "Home");
        }
    }
}
