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
        public IActionResult Add()
        {
            var model = new TeamAddModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TeamAddModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await teamService.TeamExists(model.Id))
                throw new ArgumentException("The team exist, try with another");

            await teamService.AddAsync(model);

            return RedirectToAction(nameof(All));
        }
    }
}
