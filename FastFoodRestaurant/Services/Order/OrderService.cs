
using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Client;
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

            var clientInfoModel = client.ShowInformation(userId);

            if (clientOrders.Count() == 0)
            {
                Data.Models.Order order = new()
                {
                    ClientId = userId,
                    ClientAddress = clientInfoModel.Address,
                    ClientName = clientInfoModel.Name,
                    ClientPhone = clientInfoModel.PhoneNumber
                };

                OrderItem orderItem = new()
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
                Data.Models.Order order = new()
                {
                    ClientId = userId,
                    ClientAddress = clientInfoModel.Address,
                    ClientName = clientInfoModel.Name,
                    ClientPhone = clientInfoModel.PhoneNumber
                };

                OrderItem orderItem = new()
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
                    OrderItem orderItem = new()
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

        public bool? Cart(string userId, OrderListingModel orderModel)
        {

            var clientOrder = data.Orders.Where(x => 
            x.Client.Id == userId && 
            x.IsCompleted == false)
                .FirstOrDefault();


            var clientInfoModel = client.ShowInformation(userId);



            if (clientOrder == null)
            {
                Data.Models.Order order = new()
                {
                    ClientId = userId,
                   ClientAddress = clientInfoModel.Address,
                   ClientName = clientInfoModel.Name,
                    ClientPhone = clientInfoModel.PhoneNumber
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

                ItemListingModel itemListing = new()
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

        public List<OrderHistoryModel> OrderHistory()
        {

            var orders = data.Orders.Where(x => x.IsCompleted == true).ToList();


            var allOrderHistory = Orders(orders);

            List<OrderHistoryModel> ordersHistoryList = new();
            foreach (var order in allOrderHistory)
            {

                OrderHistoryModel aohm = new();

                aohm.FullPrice = order.Model.FullPrice;
                aohm.Items = order.Model.Items;
                aohm.OrderDate = order.Model.OrderDate;



                aohm.ClientInfoModel.Address = order.Model.ClientInfoModel.Address;
                aohm.ClientInfoModel.Name = order.Model.ClientInfoModel.Name;
                aohm.ClientInfoModel.PhoneNumber = order.Model.ClientInfoModel.PhoneNumber;


                ordersHistoryList.Add(aohm);
            }

            return ordersHistoryList;

        }


        public List<OrderHistoryModel> MyOrderHistory(string userId)
        {
            var orders = data.Orders.Where(x => x.ClientId == userId && x.IsCompleted == true).ToList();

            var orderHisory = Orders(orders);

            List<OrderHistoryModel> orderHistoryList = new();

            var userInfo = data.Clients.Where(x => x.Id == userId).Select(x => new InformationModel
            {
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address
            }).FirstOrDefault();

            foreach (var order in orderHisory)
            {
                OrderHistoryModel ohm = new();
                ohm.FullPrice = order.Model.FullPrice;
                ohm.Items = order.Model.Items;
                ohm.OrderDate = order.Model.OrderDate;
                ohm.ClientInfoModel.Name = order.Model.ClientInfoModel.Name;
                ohm.ClientInfoModel.Address = order.Model.ClientInfoModel.Address;
                ohm.ClientInfoModel.PhoneNumber = order.Model.ClientInfoModel.PhoneNumber;

                orderHistoryList.Add(ohm);
            }

            return orderHistoryList;

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

        private List<OrderHistoryWithUserIdModel> Orders(List<Data.Models.Order> orders)
        {

            List<OrderHistoryWithUserIdModel> allOrders = new();
            foreach (var order in orders)
            {
                var quantityAndItemId = data.OrderItems.Where(x => x.OrderId == order.Id).Select(x => new { x.Quantity, x.ItemId }).ToList();
                OrderHistoryWithUserIdModel ordersHistories = new();

                foreach (var item in quantityAndItemId)
                {
                    var theItem = data.Items.Where(x => x.Id == item.ItemId).Select(x => new ItemListingModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = (x.Price * item.Quantity),
                        Quantity = item.Quantity

                    }).FirstOrDefault();
                    ordersHistories.Model.Items.Add(theItem);
                    ordersHistories.Model.ClientInfoModel.PhoneNumber = order.ClientPhone;
                    ordersHistories.Model.ClientInfoModel.Address = order.ClientAddress;
                    ordersHistories.Model.ClientInfoModel.Name = order.ClientName;


                }

                ordersHistories.Model.OrderDate = order.OrderDate;
                ordersHistories.UserId = order.ClientId;
                ordersHistories.Model.FullPrice = order.TotalSum;

                allOrders.Add(ordersHistories);
            }

            return allOrders;
        }





        public bool? CompleteOrder(int orderId, decimal totalSum, string userId)
        {
            var orderFromDb = data.Orders.Where(x => x.Id == orderId && x.ClientId == userId).FirstOrDefault();

            var infoModel = client.ShowInformation(userId);

            if (infoModel.Address is null ||
                infoModel.Name is null ||
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

            orderFromDb.ClientName = infoModel.Name;
            orderFromDb.ClientAddress = infoModel.Address;
            orderFromDb.ClientPhone = infoModel.PhoneNumber;

            data.SaveChanges();

            return true;

        }


        private OrderItem ShowOrderItem(int itemId, int orderId)
        => data.OrderItems.Where(x => x.ItemId == itemId && x.OrderId == orderId).FirstOrDefault();
    }
}
