using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Item;
using FastFoodRestaurant.Models.Order;
using FastFoodRestaurant.Services.Client;
using FastFoodResturant.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FastFoodRestaurant.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext data;

        private readonly IClientService client;
        public OrderService(ApplicationDbContext data, IClientService client)
        {
            this.data = data;
            this.client = client;
        }
        public void OrderNow(string userId, int itemId)
        {
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
                Data.Models.Order order = new Data.Models.Order()
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
                Data.Models.Order order = new Data.Models.Order()
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
                var orderItema = ShowOrderItem(itemId, clientOrder.Id);

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
        }

        public bool? MinusQuantity(int itemId, int orderId)
        {
            var oi = ShowOrderItem(itemId, orderId);

            if (oi == null)
            {
                return null;
            }
            if (oi.Quantity == 1)
            {
                return false;
            }

            oi.Quantity--;
            data.SaveChanges();

            return true;
        }

        public bool? PlusQuantity(int itemId, int orderId)
        {
            var oi = ShowOrderItem(itemId, orderId);

            if (oi == null)
            {
                return null;
            }
           

            oi.Quantity++;
            data.SaveChanges();

            return true;
        } 
        
        public bool? Remove(int itemId, int orderId)
        {
            var oi = ShowOrderItem(itemId, orderId);

            if (oi == null)
            {
                return null;
            }


            data.OrderItems.Remove(oi);
            data.SaveChanges();

            return true;
        }

        public bool? Cart(string userId , OrderListingModel orderModel)
        {

            var clientOrders = data.Orders.Where(x => x.Client.Id == userId).ToList();

            var clientOrder = clientOrders.Where(x => x.IsCompleted == false).FirstOrDefault();

            if (clientOrder == null)
            {
                Data.Models.Order order = new Data.Models.Order()
                {
                    ClientId = userId
                };
                data.Orders.Add(order);
                data.SaveChanges();

                return null;
            }

            var itemIds = data.OrderItems.Where(x => x.OrderId == clientOrder.Id)
                .Select(x => new { x.ItemId, x.Quantity })
                .ToList();



            foreach (var itemIdAndQuantity in itemIds)
            {

                if (!(data.Items.Any(x => x.Id == itemIdAndQuantity.ItemId)))
                {
                    throw new ArgumentException();
                }
                var item = data.Items.Where(x => x.Id == itemIdAndQuantity.ItemId).FirstOrDefault();

                ItemListingModel itemListing = new ItemListingModel
                {
                    Id = itemIdAndQuantity.ItemId,
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = itemIdAndQuantity.Quantity

                };


                orderModel.Items.Add(itemListing);
            }

            orderModel.TotalSum = orderModel.Items.Sum(x => x.Price * x.Quantity);
            orderModel.Id = clientOrder.Id;

            return true;



        }

        public List<OrderHistoryModel> MyOrderHistory(string userId)
        {
            var orders = data.Orders.Where(x => x.ClientId == userId && x.IsCompleted == true).ToList();


            List<OrderHistoryModel> allOrders = new List<OrderHistoryModel>();
            foreach (var order in orders)
            {
                var quantityAndItemId = data.OrderItems.Where(x => x.OrderId == order.Id).Select(x => new { x.Quantity, x.ItemId }).ToList();
                OrderHistoryModel ordersHistories = new OrderHistoryModel();

                foreach (var item in quantityAndItemId)
                {
                    var theItem = data.Items.Where(x => x.Id == item.ItemId).Select(x => new ItemListingModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = (x.Price * item.Quantity),
                        Quantity = item.Quantity

                    }).FirstOrDefault();

                    ordersHistories.Items.Add(theItem);
                    ordersHistories.OrderDate = order.OrderDate;
                }


                ordersHistories.FullPrice = order.TotalSum;

                allOrders.Add(ordersHistories);
            }

            return allOrders;
        }

        public List<OrderHistoryModel> FilterDate(List<OrderHistoryModel> collection, string stringDate)
        {   
            
            var date = Convert.ToDateTime(
                DateTime.ParseExact(stringDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));



            return collection.Where(x => x.OrderDate.Year == date.Year 
            && x.OrderDate.Month == date.Month
             && x.OrderDate.Day == date.Day)
                .ToList();

        }



        public bool? CompleteOrder(int orderId, decimal totalSum, string userId)
        {
            var orderFromDb = data.Orders.Where(x => x.Id == orderId).FirstOrDefault();

            var infoModel = client.ShowInformation(userId);

            if (infoModel.Address is null ||
                infoModel.Name is null||
                infoModel.PhoneNumber is null)
            {
                return false;
            }

            if (orderFromDb == null)
            {
                return null;
            }



            orderFromDb.IsCompleted = true;
            orderFromDb.TotalSum = totalSum;

            if (orderFromDb.TotalSum == 0)
            {
                return false;
            }

            orderFromDb.OrderDate = DateTime.Now;
            data.SaveChanges();

            return true;

        }


        private OrderItem ShowOrderItem(int itemId , int orderId)
        => data.OrderItems.Where(x => x.ItemId == itemId && x.OrderId == orderId).FirstOrDefault();
    }
}
