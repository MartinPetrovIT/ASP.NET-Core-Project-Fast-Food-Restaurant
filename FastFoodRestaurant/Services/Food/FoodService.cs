using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastFoodRestaurant.Areas.Admin.Models.Food;
using FastFoodRestaurant.Models.Food;
using FastFoodRestaurant.Models.Home;
using FastFoodRestaurant.Services.Image;
using FastFoodResturant.Data;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace FastFoodRestaurant.Services.Food
{
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext data;

        private readonly IMapper mapper;

        private readonly IImageService imageService;

        public FoodService(ApplicationDbContext data, IMapper mapper, IImageService imageService)
        {
            this.data = data;
            this.mapper = mapper;
            this.imageService = imageService;
        }
        
        public FoodSearchModel All(
            string searchTerm,
            string category,
            FoodSorting foodSorting,
            int currentPage ,
            int entityPerPage)
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
                .ProjectTo<FoodServiceListingModel>(mapper.ConfigurationProvider)
                .ToList();


            var foodCategories = data.FoodCategories.Select(x => x.Name);

          
            return new FoodSearchModel
            {
                Food = foods,
                TotalFood = totalFood,
                CurrentPage = currentPage,
                Categories = foodCategories

            };
        }

       void IFoodService.Add(
            string name,
            IFormFile image,
            decimal price,
            string description,
            int categoryId,
            int itemId)
        {

            var path = imageService.Upload(image);

            var food = new Data.Models.Food()
            {
                Name = name,
                ImageFileName = path,
                Price = price,
                Description = description,
                CategoryId = categoryId,
                ItemId = itemId

            };

            data.Foods.Add(food);
            data.SaveChanges();

         
        }

        public bool CheckCategoryId(int categoryId)
        {
            if (!this.data.FoodCategories.Any(c => c.Id == categoryId))
            {
                return false;
            }
              return true;

        }

        public FoodFormModel ShowFoodToEdit(int foodId)
        {
           

            var listOfCategories = GetFoodCategories();

            
            var foodModel = data.Foods.Where(x => x.Id == foodId)
                .ProjectTo<FoodFormModel>(this.mapper.ConfigurationProvider)
               .FirstOrDefault();

            foodModel.Categories = listOfCategories;

            return foodModel;

           

        }

        public int EditFood(int foodId, string name, IFormFile image, decimal price, string description, int categoryId)
        {

            var listOfCategories = GetFoodCategories();


            var path = "";

            var foodModel = data.Foods.Where(x => x.Id == foodId).FirstOrDefault();
            if (image is null)
            {
                path = foodModel.ImageFileName;
            }
            else
            {
                path = imageService.Upload(image);
                imageService.DeleteImage(foodModel.ImageFileName);
            }

            foodModel.Name = name;
            foodModel.ImageFileName = path;
            foodModel.Price = price;
            foodModel.Description = description;
            foodModel.CategoryId = categoryId;

            data.SaveChanges();

            return foodModel.ItemId;

        }

        public int Delete(int id)
        {
            var food = data.Foods.Where(x => x.Id == id).FirstOrDefault();

            var itemId = food.ItemId;

            data.Foods.Remove(food);

            data.SaveChanges();

            return itemId;
        }


        public List<HomeListingFoodModel> TakeLastAddedFoods()
        {
            var foods = data.Foods.OrderByDescending(x => x.Id)
           .ProjectTo<HomeListingFoodModel>(this.mapper.ConfigurationProvider)
           .Take(3)
           .ToList();

            return foods;
        }
        public List<Models.FoodCategory.FoodCategoryModel> GetFoodCategories()
        {
            List<Models.FoodCategory.FoodCategoryModel> categories =
              data.FoodCategories
              .ProjectTo<Models.FoodCategory.FoodCategoryModel>(mapper.ConfigurationProvider)
              .ToList();

            return categories;
        }

       
    }
}
