using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.BasketballPlayer;
using FantasyBasketballLeague.Core.Models.Teams;
using FantasyBasketballLeague.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Controllers
{
    [Authorize]
    public class BasketballPlayerController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IPositionService positionService;
        private readonly ITeamService teamService;
        private readonly INotyfService notyfService;

        public BasketballPlayerController(IPlayerService _playerService,
                                          IPositionService _positionService,
                                          ITeamService _teamService,
                                          INotyfService _notyService)
        {
            playerService = _playerService;
            positionService = _positionService;
            teamService = _teamService;
            notyfService = _notyService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new BasketballPlayerViewModel()
            {
                Positions = await positionService.GetAllPositionsAsync(),
                Teams = await teamService.GetAllTeamsAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BasketballPlayerViewModel model)
        {
            if (await playerService.PlayerNameExists(model.FirstName, model.LastName))
            {
                ModelState.AddModelError(nameof(model.Id), $"There is already a player with name {model.FirstName} {model.LastName}.");
                notyfService.Error($"There is already a player with name {model.FirstName} {model.LastName}", 10);
            }

            if ((await positionService.GetAllPositionsAsync()).Any(x => x.Name != model.Position))
            {
                ModelState.AddModelError(nameof(model.PositionId), "There's no such position.");
                notyfService.Error("There's no such position", 10);
            }

            if (!ModelState.IsValid)
            {

                model.Positions = await positionService.GetAllPositionsAsync();
                model.Teams = await teamService.GetAllTeamsAsync();

                return View(model);
            }

            var playerId = playerService.AddAsync(model);

            return RedirectToAction(nameof(Details), new { playerId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var player = await playerService.GetByIdAsync(id);

            if (player == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(player);
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var players = await playerService.GetAllPlayersAsync();

            return View(players);
        }
    }
}
