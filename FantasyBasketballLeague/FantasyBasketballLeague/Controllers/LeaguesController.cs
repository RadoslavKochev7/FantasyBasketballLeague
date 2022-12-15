using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.League;
using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Controllers
{
    public class LeaguesController : BaseController
    {
        private readonly ILeagueService leagueService;
        private readonly INotyfService notyfService;
        private readonly ITeamService teamService;

        public LeaguesController(ILeagueService _leagueService,
                                 INotyfService _notyfService,
                                 ITeamService _teamService)
        {
            leagueService = _leagueService;
            notyfService = _notyfService;
            teamService = _teamService;
        }

        public IActionResult Create()
        {
            var model = new LeagueViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeagueViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var id = await leagueService.AddAsync(model);
                notyfService.Success($"League {model.Name} is created.");

                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> All()
        {
            var leagues = await leagueService.GetAllLeaguesAsync();

            return View(leagues);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var league = await leagueService.GetByIdAsync(id);
                var model = new LeagueViewModel()
                {
                    Name = league.Name,
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LeagueViewModel model)
        {
            try
            {
                var league = await leagueService.GetByIdAsync(id);

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await leagueService.Edit(id, model);
                notyfService.Success($"League {model.Name} was successfully edited");

                return RedirectToAction(nameof(Details), new { model.Id });
            }
            catch (Exception)
            {
                notyfService.Error($"Edit [{id}] failed");
                return RedirectToAction("Error", "Home");
            }
        }
           
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var league = await leagueService.GetByIdAsync(id);
                return View(league);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var league = await leagueService.GetByIdAsync(id);

                if (league.Teams.Any())
                {
                    notyfService.Success($"League - {league.Name} cannot be deleted.There are {league.Teams.Count()} to be removed first.");
                    return RedirectToAction(nameof(All));
                }

                await leagueService.DeleteAsync(id);
                notyfService.Success($"League - {league.Name} was successfully deleted!");
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                notyfService.Error($"Delete [{id}] failed");
                return RedirectToAction("Error", "Home");
            }
        }
       
        [HttpGet]
        public async Task<IActionResult> AddTeams()
        {
            var model = await teamService.GetAllTeamsWithoutLeagues();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeams(int id, LeagueAddTeamsModel model)
        {
            var teamIds = model.Teams.Select(t => t.Id).ToArray();
            var result = await leagueService.AddTeams(teamIds, id);
            var league = await leagueService.GetByIdAsync(id);
            notyfService.Success($"[{result}] teams added to {league.Name}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
