using FastFoodRestaurant.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Services.Order
{
    public interface IOrderService
    {
        void OrderNow(string userId, int itemId);

        public bool? MinusQuantity(int itemId, int orderId);

        public bool? PlusQuantity(int itemId, int orderId);

        public bool? Remove(int itemId, int orderId);

        public bool? Cart(string userId, OrderListingModel orderModel);

        public bool? CompleteOrder(int orderId, decimal totalSum);

        public List<OrderHistoryModel> MyOrderHistory(string userId);
    }
}
