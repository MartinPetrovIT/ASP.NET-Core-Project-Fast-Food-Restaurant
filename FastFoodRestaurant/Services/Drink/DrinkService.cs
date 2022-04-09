using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastFoodRestaurant.Areas.Admin.Models.Drink;
using FastFoodRestaurant.Infrastructure;
using FastFoodRestaurant.Models.Drink;
using FastFoodRestaurant.Services.Image;
using FastFoodResturant.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using static FastFoodRestaurant.Infrastructure.ApplicationBuilderExtensions;

namespace FastFoodRestaurant.Services.Drink
{
    public class DrinkService : IDrinkService
    {

        public DrinkService(ApplicationDbContext data, IMapper mapper, IImageService imageService)
        {
            this.data = data;
            this.mapper = mapper;
            this.imageService = imageService;
        }
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;
        private readonly IImageService imageService;
        public DrinkSearchModel All(string searchTerm, bool alcoholFreeOnly)
        {
            var drinkQuery = data.Drinks.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                drinkQuery = drinkQuery.Where(f =>
                f.Name.ToLower().Contains(searchTerm.ToLower())
                );

            }
            if (alcoholFreeOnly == true)
            {
                drinkQuery = drinkQuery.Where(x => x.IsAlcoholic == false);
            }

            var drinks = drinkQuery
                .ProjectTo<DrinkListingModel>(mapper.ConfigurationProvider)
                .ToList();


            return new DrinkSearchModel
            {
                Drink = drinks,
                SearchTerm = searchTerm,
                AlcoholFreeOnly = alcoholFreeOnly
            };
        }

        public void Add(
            string name,
            decimal price, 
            IFormFile image,
            bool isAlcoholic, 
            int itemId)
        {

            var path = imageService.Upload(image);

            var drink = new Data.Models.Drink()
            {
                Name = name,
                Price = price,
                ImageFileName = path,
                IsAlcoholic = isAlcoholic,
                ItemId = itemId

            };



            data.Drinks.Add(drink);



            data.SaveChanges();
        }
        public DrinkFormModel ShowDrinkToEdit(int drinkId)
        {
            var drinkModel = data.Drinks.Where(x => x.Id == drinkId)
                .ProjectTo<DrinkFormModel>(mapper.ConfigurationProvider)
                .FirstOrDefault();  

            return drinkModel;

        }

        public int EditDrink( int id,
            string name,
            decimal price,
            IFormFile image,
            bool isAlcoholic)
        {

            var path = "";

            var drink = data.Drinks.Where(x => x.Id == id).FirstOrDefault();
            if (image is null)
            {
                path = drink.ImageFileName;
            }
            else
            {
                path = imageService.Upload(image);
                imageService.DeleteImage(drink.ImageFileName);
            }

            drink.Name = name;
            drink.Price = price;
            drink.ImageFileName = path;
            drink.IsAlcoholic = isAlcoholic;

            data.SaveChanges();

            return drink.ItemId;
        }

        public int Delete(int id)
        {
            var drink = data.Drinks.Where(x => x.Id == id).FirstOrDefault();
            var itemId = drink.ItemId;

            imageService.DeleteImage(drink.ImageFileName);

            data.Drinks.Remove(drink);

            data.SaveChanges();

            return itemId;
        }
    }
}
