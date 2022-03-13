using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Models.Order
{
    public class OrderHistoryWithUserIdModel
    {
        public OrderHistoryModel Model = new ();

        public string UserId { get; set; }

    }
}
