using FastFoodRestaurant.Areas.Admin.Models.Drink;
using FastFoodRestaurant.Services.Drink;
using FastFoodRestaurant.Services.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Areas.Admin.Controllers
{

    public class DrinkController : AdminController
    {
        public DrinkController(IItemService items, IDrinkService drinks)
        {
            this.items = items;
            this.drinks = drinks;
        }


        private readonly IItemService items;
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
            if (!ModelState.IsValid)
            {
                return View(drinkFromModel);

            }

            var itemId = items.Add(drinkFromModel.Name, drinkFromModel.Price);

            drinks.Add(
                drinkFromModel.Name,
                drinkFromModel.ImageUrl,
                drinkFromModel.Price,
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
          

            if (!ModelState.IsValid)
            {
                return View(drinkFromModel);

            }


            var itemId = drinks.EditDrink(id,
                drinkFromModel.Name,
                drinkFromModel.ImageUrl,
                drinkFromModel.Price,
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
