using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastFoodRestaurant.Areas.Admin.Models.Drink;
using FastFoodRestaurant.Models.Drink;
using FastFoodResturant.Data;
using System.Linq;

namespace FastFoodRestaurant.Services.Drink
{
    public class DrinkService : IDrinkService
    {
        public DrinkService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;
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
            string imageUrl, 
            decimal price, 
            bool isAlcoholic, 
            int itemId)
        {
            var drink = new Data.Models.Drink()
            {
                Name = name,
                ImageUrl = imageUrl,
                Price = price,
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
            string imageUrl,
            decimal price,
            bool isAlcoholic)
        {
            var drink = data.Drinks.Where(x => x.Id == id).FirstOrDefault();

            drink.Name = name;
            drink.ImageUrl = imageUrl;
            drink.Price = price;
            drink.IsAlcoholic = isAlcoholic;

            data.SaveChanges();

            return drink.ItemId;
        }

        public int Delete(int id)
        {
            var drink = data.Drinks.Where(x => x.Id == id).FirstOrDefault();
            var itemId = drink.ItemId;

            data.Drinks.Remove(drink);

            data.SaveChanges();

            return itemId;
        }
    }
}
