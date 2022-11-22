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

        public async Task<IActionResult> Details(int coachId)
        {
            var coach = await coachService.GetByIdAsync(coachId);

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

            var coachId = await coachService.AddAsync(model);
            notyfService.Success($"Успешно създадохте треньор {model.FirstName} {model.LastName}");
            return RedirectToAction(nameof(Details), new { coachId });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
           var coaches = await coachService.GetAllCoachesAsync();
           return View(coaches);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int coachId)
        {
            var coach = await coachService.GetByIdAsync(coachId);

            if (await CoachExists(coachId) == false)
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
        public async Task<IActionResult> Edit(int coachId, CoachDetailsModel model)
        {
            if (coachId != model.Id)
            {
                notyfService.Error($"Coach with Id {coachId} does not exist");
                return RedirectToAction(nameof(All));
            }

            if (await CoachExists(coachId) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await coachService.Edit(coachId, model);

            return RedirectToAction(nameof(Details), new { model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int coachId)
        {
            
            if (await CoachExists(coachId) == false)
            {
                return RedirectToAction(nameof(All));
            }
            var coach = await coachService.GetByIdAsync(coachId);

            var model = new CoachDetailsModel()
            {
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                ImageUrl = coach.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int coachId, CoachDetailsModel model)
        {
            if (await CoachExists(coachId) == false)
            {
                return RedirectToAction(nameof(All));
            }

            await coachService.DeleteAsync(coachId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Assign(int coachId)
        {
            if (await CoachExists(coachId) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var coach = await coachService.GetByIdAsync(coachId);

            var model = new CoachAssignToTeamModel()
            {
                Teams = await teamService.GetAllTeamsAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> Assign(CoachAssignToTeamModel model, int coachId)
        {
            var coach = await coachService.GetByIdAsync(coachId);

            if (await CoachExists(coachId) == false)
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

        private async Task<bool> CoachExists(int coachId)
        {
            var coach = await coachService.GetByIdAsync(coachId);
            bool exists = true;

            if (coach == null)
            {
                notyfService.Error($"Треньор с Id - {coachId} не съществува!");
                return false;
            }

            return exists;
        }
    }
}
