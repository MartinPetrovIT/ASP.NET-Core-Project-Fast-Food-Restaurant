using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastFoodRestaurant.Data.DataConstants.Client;
namespace FastFoodRestaurant.Data.Models
{
    public class Client : IdentityUser
    {
        public override string Id { get; set; }

        [MaxLength(maxClientNameLength)]
        public string Name { get; set; }
        
        [MaxLength(maxAddressLength)]
        public string Address { get; set; }

      
        public override string PhoneNumber { get; set ; }


        public List<Order> Orders = new();

    }
}
