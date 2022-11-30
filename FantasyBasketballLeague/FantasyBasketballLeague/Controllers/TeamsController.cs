using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.BasketballPlayer;
using FantasyBasketballLeague.Core.Models.Teams;
using FantasyBasketballLeague.Core.Models.UserTeams;
using FantasyBasketballLeague.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FantasyBasketballLeague.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly ITeamService teamService;
        private readonly INotyfService notyfService;

        public TeamsController(ITeamService _teamService,
                               INotyfService _notyfService)
        {
            teamService = _teamService;
            notyfService = _notyfService;
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
            {
                ModelState.AddModelError(nameof(model.Id), $"There is already a team with name {model.Name}.");
                notyfService.Error($"There is already a team with name {model.Name}.", 10);
            }

            if (!ModelState.IsValid)
            {

                model.Leagues = await teamService.GetAllLeaguesAsync();
                model.Coaches = await teamService.GetAllCoachesAsync();

                return View(model);
            }

            await teamService.AddAsync(model);
            notyfService.Success($"{model.Name} is Successfully created!");

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Mine()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var myTeams = await teamService.GetMyTeams(userId);

            return View(myTeams);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var team = await teamService.GetByIdAsync(id);

            if (team == null)
            {
                notyfService.Warning($"There's no team with id {id}");
                return RedirectToAction(nameof(All));
            }

            var model = new TeamAddModel()
            {
                Id = id,
                Name = team.Name,
                OpenPositions = team.OpenPositions,
                LogoUrl = team.LogoUrl,
                Coach = team.CoachName,
                CoachId = team.CoachId,
                League = team.League,
                LeagueId = team.LeagueId,
                Coaches = await teamService.GetAllCoachesAsync(),
                Leagues = await teamService.GetAllLeaguesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TeamAddModel model)
        {
            if (id == model.Id)
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Wrong data.");

                    model.Coaches = await teamService.GetAllCoachesAsync();
                    model.Leagues = await teamService.GetAllLeaguesAsync();

                    return View(model);
                }

                await teamService.Edit(id, model);
            }
            return RedirectToAction(nameof(All), new { model.Id });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await teamService.GetByIdAsync(id);

            if (team is null)
            {
                throw new ArgumentNullException($"There's no team with Id {id}");
            }

            await teamService.DeleteAsync(id);
            notyfService.Success($"Team {team.Name} was successfully deleted!");

            return RedirectToAction(nameof(All));
        }

        private string GetUserId()
        => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
