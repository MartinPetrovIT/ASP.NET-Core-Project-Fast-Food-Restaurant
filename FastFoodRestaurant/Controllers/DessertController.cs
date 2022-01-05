using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Controllers
{
    public class DessertController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

       
    }
}
