using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Models;
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

        public IActionResult Index()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return View();
            }

            return Redirect("/User/Login/");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}