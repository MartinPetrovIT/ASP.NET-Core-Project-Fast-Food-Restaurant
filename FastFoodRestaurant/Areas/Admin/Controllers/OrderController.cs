using FastFoodRestaurant.Services.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using static FastFoodRestaurant.Areas.Admin.AdminConstants;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFoodRestaurant.Models.Order;

using static FastFoodRestaurant.WebConstants;

namespace FastFoodRestaurant.Areas.Admin.Controllers
{
    public class OrderController : AdminController
    {
        public OrderController(IOrderService order)
        {
            this.order = order;
        }
        private readonly IOrderService order;

        [Authorize(Roles = Administrator)]
        public IActionResult OrderHistory(string dDate)
        {
            if (dDate is null)
            {
                dDate = DateTime.UtcNow.ToString("dd/MM/yyyy");
            }

            var model = order.OrderHistory();

            List<OrderHistoryModel> filteredOrders;
            try
            {
                filteredOrders = order.FilterDate(model, dDate);
            }
            catch (Exception)
            {
                dDate = DateTime.UtcNow.ToString("dd/MM/yyyy");
                filteredOrders = order.FilterDate(model, dDate);
                TempData[GlobalWarningMessageKey] = InvalidDate;
            }

            return View(filteredOrders);
        }
    }
}
