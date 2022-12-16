using AspNetCoreHero.ToastNotification.Abstractions;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using FantasyBasketballLeague.Models.User.LoginViewModel;
using FantasyBasketballLeague.Models.User.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.RoleConstants;

namespace FantasyBasketballLeague.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly INotyfService notyfService;

        public UserController(UserManager<ApplicationUser> userManager,
               SignInManager<ApplicationUser> signInManager,
               INotyfService notyfService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.notyfService = notyfService;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = new ApplicationUser()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Username
            };

            var result = await userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "User");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = await userManager.FindByNameAsync(loginViewModel.Username);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, true);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    notyfService.Information("Welcome to Fantasy League!", 3);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login");
            }

            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BecomeAdmin()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await userManager.FindByIdAsync(userId);
           
            if (user != null)
            {
                if (!await userManager.IsInRoleAsync(user, Administrator))
                {
                    await userManager.AddToRoleAsync(user, Administrator);
                    notyfService.Success($"Congratulations, you are now an {Administrator}!");

                    return RedirectToAction("Index", "Home");
                }

                notyfService.Information($"You are already an {Administrator}!");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LeaveAdmin()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                if (await userManager.IsInRoleAsync(user, Administrator))
                {
                    await userManager.RemoveFromRoleAsync(user, Administrator);
                    notyfService.Success($"Congratulations, you are no longer an {Administrator}!");

                    return RedirectToAction("Index", "Home");
                }

                notyfService.Information($"You are not an {Administrator}!");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

