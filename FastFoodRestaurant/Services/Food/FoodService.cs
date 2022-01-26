using FastFoodRestaurant.Models.Food;
using FastFoodResturant.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFoodRestaurant.Services.Food;
using FastFoodRestaurant.Services.FoodCategory;

namespace FastFoodRestaurant.Services.Food
{
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext data;

        public FoodService(ApplicationDbContext data)
        {
            this.data = data;
        }
        
        public FoodSearchModel All(
            string searchTerm,
            string category,
            FoodSorting foodSorting,
            int currentPage = 1,
            int entityPerPage = 4)
        {

            var foodQuery = data.Foods.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                foodQuery = foodQuery.Where(f =>
                f.Name.ToLower().Contains(searchTerm.ToLower()) ||
                f.Description.ToLower().Contains(searchTerm.ToLower()) ||
                f.Category.Name.ToLower().Contains(searchTerm.ToLower())
                );

            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                foodQuery = foodQuery.Where(c => c.Category.Name == category);

            }

            foodQuery = foodSorting switch
            {
                FoodSorting.PriceAcsending => foodQuery.OrderBy(x => x.Price),
                FoodSorting.PriceDescending => foodQuery.OrderByDescending(x => x.Price),
                _ => foodQuery

            };

            var totalFood = foodQuery.Count();

            var foods = foodQuery
                .Skip((currentPage - 1) * FoodSearchModel.EntityPerPage)
                .Take(FoodSearchModel.EntityPerPage)
                .Select(x => new FoodServiceListingModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price,
                    Description = x.Description,
                    Category = x.Category.Name,
                    ItemId = x.ItemId

                }).ToList();


            var foodCategories = data.FoodCategories.Select(x => x.Name);

          
            return new FoodSearchModel
            {
                Food = foods,
                TotalFood = totalFood,
                CurrentPage = currentPage,
                Categories = foodCategories

            };
        }

        //TODO: need this when make Add service model
        //private IEnumerable<FoodCategoryModel> GetFoodCategories()
        //{
        //    List<FoodCategoryModel> categories =
        //          data.FoodCategories.Select(x => new FoodCategoryModel
        //          {
        //              Id = x.Id,
        //              Name = x.Name

        //          }).ToList();

        //    return categories;
        //}
    }
}
