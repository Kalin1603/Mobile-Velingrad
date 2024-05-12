using Microsoft.AspNetCore.Mvc;
using Mobile_Velingrad.Models;
using Mobile_Velingrad.Services;
using Mobile_Velingrad.ViewModels;
using System.Diagnostics;

namespace Mobile_Velingrad.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IVehicleService _vehicleService;

        public HomeController(ILogger<HomeController> logger, IVehicleService vehicleService)
        {
            _logger = logger;
            this._vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel()
            {
                TopCheapest = await _vehicleService.SearchByPriceAsync(),
                TopExpensive = await _vehicleService.GetTopExpensiveVehiclesAsync(),
                LastAdded = await _vehicleService.GetLastAddedVehiclesAsync()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
