using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.League;
using FantasyBasketballLeague.Core.Models.Teams;
using FantasyBasketballLeague.Infrastructure.Data.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var leagues = await leagueService.GetAllLeaguesAsync();

            return View(leagues);
        }

        [HttpGet]
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
                    return RedirectToAction(nameof(Details), new {id});
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
        public async Task<IActionResult> AddTeams(int id)
        {
            var league = await leagueService.GetByIdAsync(id);
            var teams = await teamService.GetAllTeamsWithoutLeagues();
            var model = new LeagueAddTeamsModel()
            {
                LeagueId = id,
                LeagueName = league.Name, 
            };

            ViewBag.AddTeams = teams.ToList()
                .Select(t => new SelectListItem()
                {
                   Text = t.Name,
                   Value = t.Name,
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeams(int id, LeagueAddTeamsModel model)
        {
            try
            {
                var league = await leagueService.GetByIdAsync(id);
                var teams = await teamService.GetAllTeamsWithoutLeagues();
                int result = 0;

                if (model.TeamNames.Length > 0)
                {
                    teams = teams.Where(t => model.TeamNames.Contains(t.Name));
                    var teamIds = teams.Select(t => t.Id).ToArray();
                    result = await leagueService.AddTeams(teamIds, id);
                }

                notyfService.Success($"[{result}] Teams added to {league.Name}");

                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception)
            {
                notyfService.Error($"Adding teams to league - [{id}] failed");
                return RedirectToAction("Error", "Home");
            }
            
        }
    }
}
