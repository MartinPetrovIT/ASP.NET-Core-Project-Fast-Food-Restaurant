using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Client;
using FastFoodRestaurant.Models.Drink;
using FastFoodRestaurant.Models.Food;
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
        public ClientController(ApplicationDbContext data, UserManager<Client> userManager)
        {

            this.data = data;
            this.userManager = userManager;
        }
        private readonly ApplicationDbContext data;
        private readonly UserManager<Client> userManager;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Information(InformationModel informationModel)
        {
            if (!ModelState.IsValid)
            {
            
                return View(informationModel);
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var client = await userManager.FindByIdAsync(userId);

            if (client == null)
            {
                throw new ArgumentException();
            }
            else
            {
                client.Address = informationModel.Address;
                client.PhoneNumber = informationModel.PhoneNumber;
                client.Name = informationModel.Name;

            }

         


            data.SaveChanges();


            return RedirectToAction("Index","Home");
        }

        [Authorize]
        public async Task<IActionResult> Information()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var client = await userManager.FindByIdAsync(userId);

            if (client == null)
            {
               return View();
            }
            InformationModel model = new InformationModel
            {
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                Address = client.Address

            };

            return View(model);

        }

        public IActionResult Order(Item item)
        {

            return View();
        }

    }
}
