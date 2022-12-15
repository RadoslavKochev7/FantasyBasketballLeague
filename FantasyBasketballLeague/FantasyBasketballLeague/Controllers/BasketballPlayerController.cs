using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.BasketballPlayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Controllers
{
    public class BasketballPlayerController : BaseController
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

            var team = await teamService.GetByIdAsync(model.TeamId);
            if (team == null)
            {
                notyfService.Error($"There' no team with id {model.TeamId}");
                ModelState.AddModelError(nameof(team), "Invalid team");
            }

            if (team?.OpenPositions == 0)
            {
                notyfService.Warning($"There are no more open positions in team - {team.Name}");
                ModelState.AddModelError(nameof(team), "No more open positions");
            }

            if (!ModelState.IsValid)
            {
                model.Positions = await positionService.GetAllPositionsAsync();
                model.Teams = await teamService.GetAllTeamsAsync();

                return View(model);
            }

            try
            {
                var id = await playerService.AddAsync(model);
                notyfService.Success($"Succesfully created player {model.FirstName} {model.LastName}");

                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception)
            {
                notyfService.Error($"Create Team Failed");
                return RedirectToAction("BasketballPlayer", "All");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var player = await playerService.GetByIdAsync(id);
                return View(player);

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
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
            try
            {
                var player = await playerService.GetByIdAsync(id);

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
            catch (Exception)
            {
                notyfService.Error($"Edit for {id} Failed");
                return RedirectToAction("Error", "Home");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BasketballPlayerDetailsModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Wrong data.");

                    model.Teams = await teamService.GetAllTeamsAsync();
                    model.Positions = await positionService.GetAllPositionsAsync();

                    return View(model);
                }

                await playerService.Edit(id, model);
                notyfService.Information($"Player [{id}] is edited");

                return RedirectToAction(nameof(Details), new { model.Id });
            }
            catch (Exception)
            {
                notyfService.Error($"Edit for {id} Failed");
                return RedirectToAction("Error", "Home");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var player = await playerService.GetByIdAsync(id);

                await playerService.DeleteAsync(id);
                notyfService.Success($"Player {player.FirstName} {player.LastName} successfully deleted");

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                notyfService.Error($"Delete for {id} Failed");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
