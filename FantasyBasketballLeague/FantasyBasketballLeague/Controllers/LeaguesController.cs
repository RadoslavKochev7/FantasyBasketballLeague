using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Coach;
using FantasyBasketballLeague.Core.Models.League;
using FantasyBasketballLeague.Core.Models.Teams;
using FantasyBasketballLeague.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Controllers
{
    [Authorize]
    public class LeaguesController : Controller
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

            var Id = await leagueService.AddAsync(model);
            notyfService.Success($"League {model.Name} is created.");

            return RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            var leagues = leagueService.GetAllLeaguesAsync();

            return View(leagues);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var league = await leagueService.GetByIdAsync(id);

            if (league == null)
            {
                notyfService.Warning($"There's no league with Id {id}");
                return RedirectToAction(nameof(All));
            }

            var model = new LeagueViewModel()
            {
                Name = league.Name,
            };

            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, LeagueViewModel model)
        //{
        //    if (id != model.Id)
        //    {
        //        return RedirectToAction(nameof(All));
        //    }

        //    var league = await leagueService.GetByIdAsync(id);

        //    if (league == null)
        //    {
        //        notyfService.Error($"League with Id {id} does not exist");
        //        return RedirectToAction(nameof(All));
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    await leagueService.Edit(id, model);

            //return RedirectToAction(nameof(Details), new { model.Id });
        //}
    }
}
