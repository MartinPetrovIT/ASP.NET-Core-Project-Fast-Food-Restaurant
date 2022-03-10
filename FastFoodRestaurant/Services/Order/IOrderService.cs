using FastFoodRestaurant.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Services.Order
{
    public interface IOrderService
    {
        void OrderNow(string userId, 
            int itemId);

        bool? MinusQuantity(int itemId, 
            int orderId);
       
        bool? PlusQuantity(int itemId,
            int orderId);
       
        bool? Remove(int itemId,
            int orderId);
       
        bool? Cart(string userId,
            OrderListingModel orderModel);

        bool? CompleteOrder(int orderId, 
            decimal totalSum,
            string userId);


        List<OrderHistoryModel> MyOrderHistory(string userId);
       
        List<OrderHistoryModel> FilterDate(List<OrderHistoryModel> collection,
            string stringDate);
    }
}
