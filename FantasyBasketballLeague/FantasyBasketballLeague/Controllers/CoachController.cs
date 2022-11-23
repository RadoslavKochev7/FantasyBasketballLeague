using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Coach;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Controllers
{
    [Authorize]
    public class CoachController : Controller
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
            var coach = await coachService.GetByIdAsync(id);

            if (coach == null)
            {
                return RedirectToAction(nameof(Create));
            }

            return View(coach);
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
            if (!ModelState.IsValid)
                return View(model);

            var Id = await coachService.AddAsync(model);
            notyfService.Success($"Coach {model.FirstName} {model.LastName} is successfully added.");
            return RedirectToAction(nameof(Details), new { Id });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var coaches = await coachService.GetAllCoachesAsync();

            return View(coaches);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var coach = await coachService.GetByIdAsync(id);

            if (await CoachExists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var model = new CoachDetailsModel()
            {
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                ImageUrl = coach.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CoachDetailsModel model)
        {
            if (id != model.Id)
            {
                notyfService.Error($"Coach with Id {id} does not exist");
                return RedirectToAction(nameof(All));
            }

            if (await CoachExists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await coachService.Edit(id, model);

            return RedirectToAction(nameof(Details), new { model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            if (await CoachExists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }
            var coach = await coachService.GetByIdAsync(id);

            var model = new CoachDetailsModel()
            {
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                ImageUrl = coach.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CoachDetailsModel model)
        {
            if (await CoachExists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            await coachService.DeleteAsync(id);
            notyfService.Success($"Coach with Id - {id} was successfully deleted.");
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Assign(int id)
        {
            if (await CoachExists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var coach = await coachService.GetByIdAsync(id);

            var model = new CoachAssignToTeamModel()
            {
                Teams = await teamService.GetAllTeamsAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> Assign(CoachAssignToTeamModel model, int id)
        {
            var coach = await coachService.GetByIdAsync(id);

            if (await CoachExists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if (coach.Team != null)
            {
                notyfService.Warning($"Coach {coach.FirstName} {coach.LastName} already has a team assigned!");
            }

            var team = model.Teams.FirstOrDefault();

            return RedirectToAction(nameof(All));
        }

        private async Task<bool> CoachExists(int id)
        {
            var coach = await coachService.GetByIdAsync(id);
            bool exists = true;

            if (coach == null)
            {
                notyfService.Error($"Coach with Id - {id} does not exists!");
                return false;
            }

            return exists;
        }
    }
}
