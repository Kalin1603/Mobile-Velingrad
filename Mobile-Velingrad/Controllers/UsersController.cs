using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mobile_Velingrad.Data.Models;
using Mobile_Velingrad.Models;
using Mobile_Velingrad.Services;
using Mobile_Velingrad.ViewModels.Users;

namespace Mobile_Velingrad.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService userService;
        private readonly SignInManager<User> signInManager;

        public UsersController(IUsersService userService, SignInManager<User> signInManager)
        {
            this.userService = userService;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Index(int page = 1, int itemsPerPage = 10)
        {
            var model = new IndexUsersViewModel()
            {
                PageNumber = page,
                ItemsPerPage = itemsPerPage
            };
            var newModel = await userService.GetUsersAsync(model);
            return View(newModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IndexUsersViewModel model)
        {
            var newModel = await userService.GetUsersAsync(model);
            return View(newModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await userService.CreateUserAsync(model);
            return RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await userService.GetUserToEditAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await userService.EditUserAsync(model);
            return RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                bool deleted = await userService.DeleteUserAsync(id);
                if (!deleted)
                {
                    return View("Error", new ErrorViewModel { Message = "Failed to delete the user." });
                }
            }
            catch (ArgumentException ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var model = await this.userService.GetUserDetailsAsync(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            var externalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var model = new RegisterPageViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = externalLogins
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterPageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                return View(model);
            }
            var result = await userService.RegisterUserAsync(model.Register);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Registration failed.");
                model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            var externalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var model = new LoginPageViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = externalLogins
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginPageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                return View(model);
            }

            var result = await userService.LoginUserAsync(model.Login);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Invalid login.");
                model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
