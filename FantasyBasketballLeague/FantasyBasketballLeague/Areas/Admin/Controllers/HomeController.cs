﻿using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
