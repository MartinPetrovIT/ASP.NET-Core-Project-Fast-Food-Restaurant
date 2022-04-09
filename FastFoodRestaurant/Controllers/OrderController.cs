using FastFoodRestaurant.Models.Order;
using FastFoodRestaurant.Services.Client;
using FastFoodRestaurant.Services.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using static FastFoodRestaurant.WebConstants;

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






         [Authorize]
        public  IActionResult OrderNow(int itemId)
        {
           
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var clientInfo = clientService.ShowInformation(userId);
            if (clientInfo.Address == null ||
                clientInfo.Name == null ||
                clientInfo.PhoneNumber == null)
            {
                TempData[GlobalWarningMessageKey] = OrderInformationSholdbeaddedBeforeMakeAnOrder;
                return RedirectToAction("Information", "Client");
            }
            orderService.OrderNow(userId, itemId);

            TempData[GlobalMessageKey] = ItemIsAdded;

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Cart(OrderListingModel orderModel)
        {
           
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var clientInfo = clientService.ShowInformation(userId);
            if (clientInfo.Address == null ||
        clientInfo.Name == null ||
        clientInfo.PhoneNumber == null)
            {
                TempData[GlobalWarningMessageKey] = OrderInformationSholdbeaddedBeforeMakeAnOrder;
                return RedirectToAction("Information", "Client");
            }
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

        [Authorize]
        public IActionResult ChangeInformation()
        {

            return RedirectToAction("Information", "Client");
        }

        [Authorize]
        public IActionResult PlusQuantity(int itemId, int orderId)
        {
            var oi = orderService.PlusQuantity(itemId, orderId);
            if (oi == null)
            {
                return NotFound();
            }

            return RedirectToAction("Cart", "Order");
        }

        [Authorize]
        public IActionResult MinusQuantity(int itemId, int orderId)
        {
            var oi = orderService.MinusQuantity(itemId, orderId);
            if (oi == null)
            {
                return NotFound();
            }
                
            if (oi == false)
            {
               
                TempData[GlobalWarningMessageKey] = ClickRemoveButton;
                return RedirectToAction("Cart", "Order");
            }

            return RedirectToAction("Cart", "Order");
        }

        [Authorize]
        public IActionResult Remove(int itemId, int orderId)
        {
            var oi = orderService.Remove(itemId, orderId);
            if (oi == null)
            {
                return NotFound();
            }

            return RedirectToAction("Cart", "Order");
        }

        [Authorize]
        public IActionResult MyOrderHistory(string dDate)
        {
            if (dDate is null)
            {
                dDate = DateTime.UtcNow.ToString("dd/MM/yyyy");
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

           
            var allOrders = orderService.MyOrderHistory(userId);

            List<OrderHistoryModel> filteredOrders;
            try
            {
                filteredOrders = orderService.FilterDate(allOrders, dDate);
            }
            catch (Exception)
            {
                dDate = DateTime.UtcNow.ToString("dd/MM/yyyy");
                filteredOrders = orderService.FilterDate(allOrders, dDate);
                TempData[GlobalWarningMessageKey] = InvalidDate;
            }

            return View(filteredOrders);
        }
        [Authorize]
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
                TempData[GlobalWarningMessageKey] = OrderPriceAndAllFeildsMustBeFilled;

                return RedirectToAction("Cart", "Order");  
            }

            TempData[GlobalMessageKey] = CompletedOrder;

            return RedirectToAction("Index", "Home");
        }
    }
}
