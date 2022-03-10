namespace FastFoodRestaurant.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = Administrator)]
    public abstract class AdminController : Controller
    {
    }
}
