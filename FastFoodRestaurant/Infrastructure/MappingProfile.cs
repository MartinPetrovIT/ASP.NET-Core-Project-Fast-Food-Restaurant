using AutoMapper;
using FastFoodRestaurant.Areas.Admin.Models.Drink;
using FastFoodRestaurant.Areas.Admin.Models.Food;
using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Client;
using FastFoodRestaurant.Models.Drink;
using FastFoodRestaurant.Models.FoodCategory;
using FastFoodRestaurant.Models.Home;
using FastFoodRestaurant.Services.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

         CreateMap<Client, InformationModel>();

         CreateMap<Drink, DrinkListingModel>();

         CreateMap<Drink, DrinkFormModel>();

         CreateMap<Food, FoodFormModel>();

         CreateMap<Food, HomeListingFoodModel>();

         CreateMap<FoodCategory, FoodCategoryModel>();

         CreateMap<Food, FoodServiceListingModel>()
          .ForMember(c => c.Category, cfg => cfg.MapFrom(c => c.Category.Name));
        }
    }
}
