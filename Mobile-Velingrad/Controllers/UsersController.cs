using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobile_Velingrad.Models;
using Mobile_Velingrad.Services;
using Mobile_Velingrad.ViewModels.Users;

namespace Mobile_Velingrad.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService userService;

        public UsersController(IUsersService userService)
        {
            this.userService = userService;
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
            return RedirectToAction(nameof (this.Index));
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
    }
}
