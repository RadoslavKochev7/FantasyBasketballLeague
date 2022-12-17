using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Coach;
using FantasyBasketballLeague.Infrastructure.Data.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Controllers
{
    public class CoachController : BaseController
    {
        private readonly ICoachService coachService;
        private readonly INotyfService notyfService;
        private readonly ITeamService teamService;

        public CoachController(ICoachService _coachService,
                               INotyfService _notyfService,
                               ITeamService _teamService)
        {
            this.coachService = _coachService;
            this.notyfService = _notyfService;
            this.teamService = _teamService;
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var coach = await coachService.GetByIdAsync(id);
                return View(coach);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
          
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CoachViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CoachViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var Id = await coachService.AddAsync(model);
                notyfService.Success($"Coach {model.FirstName} {model.LastName} is successfully added.");

                return RedirectToAction(nameof(Details), new { Id });
            }
            catch (Exception)
            {
                notyfService.Error("Create failed");
                return RedirectToAction(nameof(All));
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var coaches = await coachService.GetAllCoachesAsync();

            return View(coaches);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var coach = await coachService.GetByIdAsync(id);
                var model = new CoachDetailsModel()
                {
                    FirstName = coach.FirstName,
                    LastName = coach.LastName,
                    ImageUrl = coach.ImageUrl,
                    TeamId = coach.TeamId,
                    Teams = await teamService.GetAllTeamsAsync()
                };

                return View(model);
            }
            catch (Exception)
            {
                notyfService.Warning("Inavalid model");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CoachDetailsModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await coachService.Edit(id, model);
                notyfService.Success($"Edit [{id}] successfull");
                return RedirectToAction(nameof(Details), new { model.Id });
            }
            catch (Exception)
            {
                notyfService.Error($"Edit [{id}] Failed");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Administrator)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var coach = await coachService.GetByIdAsync(id);

                await coachService.DeleteAsync(id);
                notyfService.Success($"Coach {coach.FirstName} {coach.LastName} was successfully deleted!");

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                notyfService.Error($"Delete [{id}] Failed");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Administrator)]
        public async Task<IActionResult> Assign(int id)
        {
            var model = new CoachAssignToTeamModel()
            {
                Teams = await teamService.GetAllTeamsWithoutCoaches()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(int id, CoachAssignToTeamModel model)
        {
            try
            {
                var coach = await coachService.GetByIdAsync(id);

                if (coach.TeamId != 0)
                {
                    notyfService.Warning($"Coach {coach.FirstName} {coach.LastName} already has a team assigned!");
                    return RedirectToAction(nameof(All));
                }
                var isAdded = await coachService.AssignToTeam(id, model.TeamId);

                if (isAdded)
                {
                    var team = await teamService.GetByIdAsync(model.TeamId);
                    notyfService.Success($"Coach {coach.FirstName} {coach.LastName} signed up with {team.Name}");
                }

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                notyfService.Error($"Assign [{id}] Failed");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
