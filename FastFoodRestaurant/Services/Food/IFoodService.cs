using FastFoodRestaurant.Areas.Admin.Models.Food;
using FastFoodRestaurant.Models.Food;
using FastFoodRestaurant.Models.FoodCategory;
using FastFoodRestaurant.Models.Home;
using System.Collections.Generic;

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

        void Add(
            string name,
            string imageUrl,
            decimal price,
            string description,
            int categoryId,
            int itemId);

        int EditFood(
            int foodId,
            string name,
            string imageUrl,
            decimal price,
            string description,
            int categoryId);

        int Delete(int id);

        bool CheckCategoryId(int categoryId);

         FoodFormModel ShowFoodToEdit(int foodId);

         List<FoodCategoryModel> GetFoodCategories();

         List<HomeListingFoodModel> TakeLastAddedFoods();

    }
}
