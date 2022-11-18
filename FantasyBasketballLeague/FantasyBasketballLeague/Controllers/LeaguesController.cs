using FantasyBasketballLeague.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Controllers
{
    [Authorize]
    public class LeaguesController : Controller
    {
        private readonly ILeagueService leagueService;

        public LeaguesController(ILeagueService _leagueService)
        {
            leagueService = _leagueService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
