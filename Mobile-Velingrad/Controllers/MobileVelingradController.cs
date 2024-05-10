using Microsoft.AspNetCore.Mvc;
using Mobile_Velingrad.Services;
using Mobile_Velingrad.ViewModels;

namespace Mobile_Velingrad.Controllers
{
    public class MobileVelingradController : Controller
    {
        private IVehicleService vehiclesService;

        public MobileVelingradController(IVehicleService vehiclesService)
        {
            this.vehiclesService = vehiclesService;
        }

        public IActionResult Index(int page = 1)
        {
            var model = vehiclesService.GetVehiclesAsync(page);
            return View(model);
        }


        public IActionResult SearchByPrice(int minPrice, int maxPrice, int page = 1)
        {
            if (minPrice !=0 && maxPrice != 0)
            {
                var model = vehiclesService.SearchByPriceAsync(minPrice, maxPrice, page);
                return View(model);

            }
            else
            {
                return View();
            }
        }
    }
}
