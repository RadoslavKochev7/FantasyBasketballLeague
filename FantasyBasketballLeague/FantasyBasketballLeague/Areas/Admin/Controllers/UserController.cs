using FantasyBasketballLeague.Areas.Admin.Models.User;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FantasyBasketballLeague.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<ApplicationUser> userManager,
               SignInManager<ApplicationUser> signInManager,
               RoleManager<IdentityRole> roleManager
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Roles(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                Username = user.UserName
            };


            ViewBag.RoleItems = roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userManager.IsInRoleAsync(user, r.Name).Result
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Roles(UserRolesViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.RoleNames?.Length > 0)
            {
                await userManager.AddToRolesAsync(user, model.RoleNames);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await userManager.Users
                .Select(u => new UserListViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    Username = u.UserName,
                })
                .ToListAsync();

            return View(users);
        }

        public IActionResult CreateRole()
        {
            var model = new IdentityRoleCreateModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRoleCreateModel model)
        {
            if (roleManager.Roles.Any(r => r.Name == model.Name))
                ModelState.AddModelError("", $"There's already a role {model.Name}");

            if (!ModelState.IsValid)
                return View(model);

            var role = new IdentityRole()
            {
                Name = model.Name,
                NormalizedName = model.Name.ToUpper()
            };

            await roleManager.CreateAsync(role);

            return RedirectToAction(nameof(ManageUsers));
        }
    }
}
