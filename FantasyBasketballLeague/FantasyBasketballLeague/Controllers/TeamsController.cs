using FantasyBasketballLeague.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamService teamService;

        public TeamsController(ITeamService _teamService)
        {
            teamService = _teamService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await teamService.GetAllTeamsAsync();

            return View(model);
        }
    }
}
