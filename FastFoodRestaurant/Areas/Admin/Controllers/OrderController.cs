using FastFoodRestaurant.Services.Order;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Areas.Admin.Controllers
{
    public class OrderController : AdminController
    {
        public OrderController(IOrderService order)
        {
            this.order = order;
        }
        private readonly IOrderService order;

        public IActionResult OrderHistory(string dDate)
        {
            if (dDate is null)
            {
                dDate = DateTime.UtcNow.ToString("dd/MM/yyyy");
            }

            var model = order.OrderHistory();

            order.FilterDate(model, dDate);

            return View(order.FilterDate(model, dDate));
        }
    }
}
