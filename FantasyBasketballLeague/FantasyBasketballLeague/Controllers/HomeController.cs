using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Areas.Admin.Constants;
using FantasyBasketballLeague.Infrastructure.Data.Constants;
using FantasyBasketballLeague.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FantasyBasketballLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly INotyfService notyfService;

        public HomeController(INotyfService _notyfService)
        {
            notyfService = _notyfService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                if (User.IsInRole(RoleConstants.Administrator))
                {
                    return RedirectToAction("Index", "Home", new { area = AdminConstants.AreaName });
                }
                return View();
            }

            notyfService.Information("Sign in and start creating your fantasy league", 10);
            return Redirect("/User/Login/");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}