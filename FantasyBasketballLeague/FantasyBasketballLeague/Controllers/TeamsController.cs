using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Teams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Controllers
{
    [Authorize]
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
            if (await teamService.TeamExists(model.Name))
                ModelState.AddModelError(nameof(model.Id), $"There is already a team with name '{model.Name}'.");

            if (!ModelState.IsValid)
            {
                model.Leagues = await teamService.GetAllLeaguesAsync();
                model.Coaches = await teamService.GetAllCoachesAsync();

                return View(model);
            }

            await teamService.AddAsync(model);

            return RedirectToAction(nameof(All));
        }
    }
}
