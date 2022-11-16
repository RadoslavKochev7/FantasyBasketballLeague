using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Teams;
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new TeamAddModel()
            {
                Leagues = await teamService.GetAllLeaguesAsync(),
                Coaches = await teamService.GetAllCoachesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamAddModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Leagues = await teamService.GetAllLeaguesAsync();
                model.Coaches = await teamService.GetAllCoachesAsync();

                return View(model);
            }

            if (await teamService.TeamExists(model.Id))
              throw new ArgumentException("The team exist, try with another");

            await teamService.AddAsync(model);

            return RedirectToAction(nameof(All));
        }
    }
}
