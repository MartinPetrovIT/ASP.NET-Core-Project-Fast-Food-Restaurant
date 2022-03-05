using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Client;
using FastFoodRestaurant.Models.Drink;
using FastFoodRestaurant.Models.Food;
using FastFoodRestaurant.Services.Client;
using FastFoodResturant.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Controllers
{
    public class ClientController : Controller
    {
        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }
        private readonly IClientService clientService;

        [HttpPost]
        [Authorize]
        public IActionResult Information(InformationModel informationModel)
        {
            if (!ModelState.IsValid)
            {
            
                return View(informationModel);
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                clientService.SetInformation(
                    userId, 
                    informationModel.Name, 
                    informationModel.PhoneNumber, 
                    informationModel.Address);

            }

            return RedirectToAction("Index","Home");
        }

        [Authorize]
        public IActionResult Information()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId == null)
            {
                throw new ArgumentNullException();
            }
            var model = clientService.ShowInformation(userId);

            return View(model);

        }

        public IActionResult Order(Item item)
        {

            return View();
        }

    }
}
