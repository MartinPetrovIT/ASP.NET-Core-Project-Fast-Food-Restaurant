using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFoodRestaurant.Models.Food;
namespace FastFoodRestaurant.Services.Food
{
     public interface IFoodService
    {
        FoodSearchModel All(
            string searchTerm,
            string category,
            FoodSorting foodSorting,
            int currentPage = 1,
            int entityPerPage = 4
            );  

    }
}
