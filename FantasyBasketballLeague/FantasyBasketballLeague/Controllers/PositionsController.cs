using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Position;
using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Controllers
{
    public class PositionsController : BaseController
    {
        private readonly IPositionService positionService;
        private readonly INotyfService notyfService;

        public PositionsController(IPositionService positionService,
                                   INotyfService notyfService)
        {
            this.positionService = positionService;
            this.notyfService = notyfService;
        }

        [HttpGet]
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

            try
            {
                var Id = await positionService.AddAsync(model);
                notyfService.Success($"Position {model.Name} - {model.Initials} is created.");
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var positions = await positionService.GetAllPositionsAsync();

            return View(positions);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var position = await positionService.GetByIdAsync(id);
                return View(position);

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var position = await positionService.GetByIdAsync(id);
                var model = new PositionViewModel()
                {
                    Name = position.Name,
                    Initials = position.Initials
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PositionViewModel model)
        {
            try
            {
                var position = await positionService.GetByIdAsync(id);

                await positionService.Edit(position.Id, model);
                notyfService.Success($"Position {model.Name} was successfully edited");

                return RedirectToAction(nameof(Details), new { model.Id });

            }
            catch (Exception)
            {
                notyfService.Warning("Editing failed!");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var position = await positionService.GetByIdAsync(id);
                await positionService.DeleteAsync(id);
                notyfService.Success($"Position {position.Name} was successfully deleted!");
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

        }
    }
}
