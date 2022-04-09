using FastFoodRestaurant.Areas.Admin.Models.Drink;
using FastFoodRestaurant.Services.Drink;
using FastFoodRestaurant.Services.Image;
using FastFoodRestaurant.Services.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FastFoodRestaurant.WebConstants;

namespace FastFoodRestaurant.Areas.Admin.Controllers
{

    public class DrinkController : AdminController
    {
        public DrinkController(IItemService items, IDrinkService drinks , IImageService image)
        {
            this.items = items;
            this.image = image;
            this.drinks = drinks;
        }


        private readonly IItemService items;
        private readonly IImageService image;
        private readonly IDrinkService drinks;

        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Add(DrinkFormModel drinkFromModel)
        {
            if (image.CheckImage(drinkFromModel.Image) is false 
                || image.CheckImage(drinkFromModel.Image) is null)
            {
                TempData[GlobalWarningMessageKey] = MessageForNullImageAndFormats;
                return View(drinkFromModel);
            }

            if (!ModelState.IsValid)
            {
                return View(drinkFromModel);

            }

            var itemId = items.Add(drinkFromModel.Name, drinkFromModel.Price);

            drinks.Add(
                drinkFromModel.Name,
                drinkFromModel.Price,
                drinkFromModel.Image,
                drinkFromModel.IsAlcoholic,
                itemId);


            return Redirect("/");
        }
        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Edit(int id)
        {
            
            var drinkModel = drinks.ShowDrinkToEdit(id);

            if (drinkModel == null)
            {
                return NotFound();
            }

         
            return View(drinkModel);
        }

        [Authorize(Roles = AdminConstants.Administrator)]
        [HttpPost]
        public IActionResult Edit(DrinkFormModel drinkFromModel, int id)
        {
            if (image.CheckImage(drinkFromModel.Image) is false)
            {
                TempData[GlobalWarningMessageKey] = MessageForFormats;
                return View(drinkFromModel);
            }

            if (!ModelState.IsValid)
            {
                return View(drinkFromModel);

            }


            var itemId = drinks.EditDrink(id,
                drinkFromModel.Name,
                drinkFromModel.Price,
                drinkFromModel.Image,
                drinkFromModel.IsAlcoholic
                );

            items.Edit(itemId, drinkFromModel.Name, drinkFromModel.Price);


            return Redirect("/");
        }

        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Delete(int id)
        {
            items.Delete(drinks.Delete(id));

            return Redirect("/");
        }
    }
}
