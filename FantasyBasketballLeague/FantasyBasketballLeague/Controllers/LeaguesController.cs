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

        public LeaguesController(ILeagueService _leagueService,
                                 INotyfService _notyfService)
        {
            leagueService = _leagueService;
            notyfService = _notyfService;
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
                return RedirectToAction("Error", "Home");
            }
        }
       
        [HttpGet]
        public async Task<IActionResult> AddTeams()
        {
            var model = await leagueService.GetAllTeamsWithoutLeague();

            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddTeams(int id, LeagueAddTeamsModel model)
        //{
        //    if (model == null)
        //        notyfService.Warning($"There are no teams without league");

        //    //var teamIds = model.Select(x => x.TeamId).ToArray();
        //    //var result = await leagueService.AddTeams(teamIds, id);
        //    //var league = await leagueService.GetByIdAsync(id);
        //    //notyfService.Success($"{result} teams are added to {league.Name}");

        //    return RedirectToAction(nameof(All));
        //}
    }
}
