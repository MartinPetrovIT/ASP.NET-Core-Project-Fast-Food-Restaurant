using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastFoodRestaurant.Data.DataConstants.Client;
namespace FastFoodRestaurant.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public List<OrderItem> Items = new();

        public decimal TotalSum { get; set; }

        public bool IsCompleted { get; set; }

        [MaxLength(maxClientNameLength)]
        [Required]
        public string ClientName { get; set; }

        [Required]
        public string ClientPhone { get; set; }

        [MaxLength(maxAddressLength)]
        [Required]
        public string ClientAddress { get; set; }

        public string ClientId { get; init; }

        public Client Client { get; set; }

        public DateTime OrderDate { get; set; }

    }
}
