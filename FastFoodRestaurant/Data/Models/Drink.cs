using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastFoodRestaurant.Data.DataConstants.Drink;
namespace FastFoodRestaurant.Data.Models
{
    public class Drink
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(maxDrinkNameLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }


        public decimal Price { get; set; }

        public bool IsAlcoholic { get; set; }

        public Item Item { get; set; }

        public int ItemId { get; set; }

    }
}
