using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.BasketballPlayer;
using FantasyBasketballLeague.Core.Models.Coach;
using FantasyBasketballLeague.Core.Services;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
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
            try
            {
                if (await playerService.PlayerNameExists(model.FirstName, model.LastName))
                {
                    ModelState.AddModelError(nameof(model.Id), $"There is already a player with name {model.FirstName} {model.LastName}.");
                    notyfService.Error($"There is already a player with name {model.FirstName} {model.LastName}", 10);
                }

                var team = await teamService.GetByIdAsync(model.TeamId);
                if (team == null)
                {
                    notyfService.Error($"There' no team with id {model.TeamId}");
                    return View(model);
                }

                if (team.OpenPositions == 0)
                {
                    notyfService.Warning($"There are no more open positions in team - {team.Name}");
                }
                if (!ModelState.IsValid)
                {
                    model.Positions = await positionService.GetAllPositionsAsync();
                    model.Teams = await teamService.GetAllTeamsAsync();

                    return View(model);
                }

                var id = await playerService.AddAsync(model);

                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Player cannot be added.", ex);
            }
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var player = await playerService.GetByIdAsync(id);

            if (player == null)
            {
                notyfService.Warning($"There's no player with id {id}");
                return RedirectToAction(nameof(All));
            }

            var model = new BasketballPlayerDetailsModel()
            {
                Id = id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                SeasonsPlayed = player.SeasonsPlayed,
                Position = player.Position,
                PositionId = player.PositionId,
                Team = player.Team,
                TeamId = player.TeamId,
                IsStarter = player.IsStarter,
                IsTeamCaptain = player.IsTeamCaptain,
                JerseyNumber = player.JerseyNumber,
                Teams = await teamService.GetAllTeamsAsync(),
                Positions = await positionService.GetAllPositionsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BasketballPlayerDetailsModel model)
        {
            if (id == model.Id)
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Wrong data.");

                    model.Teams = await teamService.GetAllTeamsAsync();
                    model.Positions = await positionService.GetAllPositionsAsync();

                    return View(model);
                }

                await playerService.Edit(id, model);
            }
                return RedirectToAction(nameof(Details), new { model.Id });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var player = await playerService.GetByIdAsync(id);

            if (player is null)
            {
                throw new ArgumentNullException($"There's no player with Id {id}");
            }

            await playerService.DeleteAsync(id);
            notyfService.Success($"Player {player.FirstName} {player.LastName} was successfully deleted!");

            return RedirectToAction(nameof(All));
        }
    }
}
