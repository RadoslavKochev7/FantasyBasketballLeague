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

        public CoachController(ICoachService coachService)
        {
            this.coachService = coachService;
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
            return RedirectToAction(nameof(Details), new { coachId });
        }
    }
}
