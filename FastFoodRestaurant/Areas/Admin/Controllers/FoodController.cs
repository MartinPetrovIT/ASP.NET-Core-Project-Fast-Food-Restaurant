using FastFoodRestaurant.Areas.Admin.Models.Food;
using FastFoodRestaurant.Services.Food;
using FastFoodRestaurant.Services.Image;
using FastFoodRestaurant.Services.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FastFoodRestaurant.WebConstants;

namespace FastFoodRestaurant.Areas.Admin.Controllers
{
    
    public class FoodController : AdminController
    {

        public FoodController(IFoodService foods, IItemService items, IImageService image)
        {
            this.foods = foods;
            this.image = image;
            this.items = items;
        }

        private readonly IFoodService foods;
        private readonly IImageService image;
        private readonly IItemService items;

        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Add()
        {
            return View(new FoodFormModel { Categories = foods.GetFoodCategories()});
        }

        [HttpPost]
        [Authorize(Roles = AdminConstants.Administrator)]
        [HttpPost]
        public IActionResult Add(FoodFormModel foodFromModel)
        {
            if (image.CheckImage(foodFromModel.Image) is false
                || image.CheckImage(foodFromModel.Image) is null)
            {
                TempData[GlobalWarningMessageKey] = MessageForNullImageAndFormats;
                foodFromModel.Categories = foods.GetFoodCategories();
                return View(foodFromModel);
            }
            if (!foods.CheckCategoryId(foodFromModel.CategoryId))
            {
                ModelState.AddModelError(nameof(foodFromModel), MessageForNotExistingCategory);
            }

            if (!ModelState.IsValid)
            {
                foodFromModel.Categories = foods.GetFoodCategories();
                return View(foodFromModel);
            }

            var itemId = items.Add(foodFromModel.Name, foodFromModel.Price);

               foods.Add(foodFromModel.Name,
               foodFromModel.Image,
               foodFromModel.Price,
               foodFromModel.Description,
               foodFromModel.CategoryId,
               itemId);

            


            return Redirect("/");
        }

        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Edit(int id)
        {
            FoodFormModel foodModel;
            try
            {
                foodModel = foods.ShowFoodToEdit(id);
            }
            catch (System.Exception)
            {

                return NotFound();
            }

          

            return View(foodModel);
        }

        [Authorize(Roles = AdminConstants.Administrator)]
        [HttpPost]
        public IActionResult Edit(FoodFormModel editedModel, int id)
        {
            if (image.CheckImage(editedModel.Image) is false)
            {
                TempData[WebConstants.GlobalWarningMessageKey] = WebConstants.MessageForFormats;
                editedModel.Categories = foods.GetFoodCategories();
                return View(editedModel);
            }

            if (!foods.CheckCategoryId(editedModel.CategoryId))
            {
                ModelState.AddModelError(nameof(editedModel), MessageForNotExistingCategory);
            }

            if (!ModelState.IsValid)
            {
                editedModel.Categories = foods.GetFoodCategories();
                return View(editedModel);
            }


            var itemId = foods.EditFood(
                id,
                editedModel.Name,
                editedModel.Image,
                editedModel.Price,
                editedModel.Description,
                editedModel.CategoryId);

            items.Edit(
                itemId, 
                editedModel.Name,
                editedModel.Price);

            return Redirect("/");

        }

        [Authorize(Roles = AdminConstants.Administrator)]
        public IActionResult Delete(int id)
        {
            items.Delete(foods.Delete(id));

            return Redirect("/");
        }
    }
}

