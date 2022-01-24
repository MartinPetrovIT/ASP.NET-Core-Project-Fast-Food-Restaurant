using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace FastFoodRestaurant.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public List<OrderItem> Items = new();

        public decimal TotalSum { get; set; }

        public bool IsCompleted { get; set; }

        public string ClientId { get; init; }

        public Client Client { get; set; }

        public DateTime OrderDate { get; set; }

    }
}
