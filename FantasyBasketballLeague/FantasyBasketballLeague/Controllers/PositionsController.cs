using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Position;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Controllers
{
    [Authorize]
    public class PositionsController : Controller
    {
        private readonly IPositionService positionService;
        private readonly INotyfService notyfService;

        public PositionsController(IPositionService positionService, 
                                   INotyfService notyfService)
        {
            this.positionService = positionService;
            this.notyfService = notyfService;
        }

        public IActionResult Create()
        {
            var model = new PositionViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PositionViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var Id = await positionService.AddAsync(model);
            notyfService.Success($"Position {model.Name} - {model.Initials} is created.");

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var positions = await positionService.GetAllPositionsAsync();

            return View(positions);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var position = await positionService.GetByIdAsync(id);

            if (position == null)
            {
                notyfService.Warning($"There's no position with Id {id}");
                return RedirectToAction(nameof(All));
            }

            var model = new PositionViewModel()
            {
                Name = position.Name,
                Initials = position.Initials
            };

            return View(model);
        }
    }
}
