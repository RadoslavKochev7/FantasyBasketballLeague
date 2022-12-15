using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Teams;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FantasyBasketballLeague.Controllers
{
    public class TeamsController : BaseController
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
        public async Task<IActionResult> Details(int id)
        {
            var team = await teamService.GetByIdAsync(id);

            if (team == null)
            {
                notyfService.Warning($"There's no team with Id {id}");
                return RedirectToAction(nameof(All));
            }

            return View(team);
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
                ModelState.AddModelError(nameof(model.Id), "");
                notyfService.Warning($"There is already a team with name {model.Name}.", 10);
            }

            if (!ModelState.IsValid)
            {
                model.Leagues = await teamService.GetAllLeaguesAsync();
                model.Coaches = await teamService.GetAllCoachesAsync();

                return View(model);
            }

            try
            {
                var id = await teamService.AddAsync(model);
                notyfService.Success($"{model.Name} is Successfully created!");

                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception)
            {
                notyfService.Error($"Create Team Failed");
                return RedirectToAction("Error", "Home");
            }
           
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
            try
            {
                var team = await teamService.GetByIdAsync(id);

                var model = new TeamAddModel()
                {
                    Id = id,
                    Name = team.Name,
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
            catch (Exception)
            {
                notyfService.Error($"Edit for [{id}] Failed");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TeamAddModel model)
        {
            try
            {
                if (id == model.Id)
                {
                    if (!ModelState.IsValid)
                    {
                        model.Coaches = await teamService.GetAllCoachesAsync();
                        model.Leagues = await teamService.GetAllLeaguesAsync();

                        return View(model);
                    }

                    await teamService.Edit(id, model);
                }
                return RedirectToAction(nameof(All), new { id });
            }
            catch (Exception)
            {
                notyfService.Error($"Edit for [{id}] Failed");
                return RedirectToAction("Error", "Home");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var team = await teamService.GetByIdAsync(id);

                await teamService.DeleteAsync(id);
                notyfService.Success($"Team {team.Name} was successfully deleted!");

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                notyfService.Error($"Delete for [{id}] Failed");
                return RedirectToAction("Error", "Home");
            }
        }

        private string GetUserId()
        => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
