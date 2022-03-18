using FastFoodRestaurant.Data.Models;
using FastFoodRestaurant.Models.Client;
using FastFoodRestaurant.Services.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

          

            string userId;

            try
            {
                userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch (System.Exception)
            {

                return NotFound();
            }

            clientService.SetInformation(
                    userId, 
                    informationModel.Name, 
                    informationModel.PhoneNumber, 
                    informationModel.Address);

            

            return RedirectToAction("Index","Home");
        }

        [Authorize]
        public IActionResult Information()
        {
            string userId;

            try
            {
                 userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch (System.Exception)
            {

                return NotFound();
            }
           
            var model = clientService.ShowInformation(userId);

            return View(model);

        }


    }
}
