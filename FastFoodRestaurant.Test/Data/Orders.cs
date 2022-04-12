using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FastFoodRestaurant.Test.Data.Client;

namespace FastFoodRestaurant.Test.Data
{
    public class Orders
    {
        public static OrderListingModel OrderListingModel
       => new()
       {
           InformationModel = new() {
           Name ="Marin",
           Address ="Marinova dolina",
           PhoneNumber ="0887766388"
           
           }
       };

        public static OrderItem OrderItem
      => new()
      {
          ItemId = 1,
          OrderId = 2,
          Quantity = 3
          
      };

        public static Order OrderWithInlineDate(string date)
     => new()
     {
         Id = 1,
         ClientId = TestDataConstants.Client.TestId,
         OrderDate = DateTime.Parse(date),
         IsCompleted = true
     };

        public static Order Order
    => new()
    {
        Id = 1,
        ClientId = TestDataConstants.Client.TestId,

    };


    }
}
