using Microsoft.AspNetCore.Mvc;
using Mobile_Velingrad.Importer;
using Mobile_Velingrad.Services;
using Mobile_Velingrad.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile_Velingrad.Controllers
{
    public class MobileVelingradController : Controller
    {
        private IVehicleService vehiclesService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MobileVelingradController(IVehicleService vehiclesService, IWebHostEnvironment hostingEnvironment)
        {
            this.vehiclesService = vehiclesService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            List<JsonVehicle> items = await LoadJsonVehiclesAsync();

            var inputModels = items.Select(MapToInputViewModel).ToList();

            int pageSize = 10;
            int totalItems = items.Count;
            var paginatedItems = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new VehiclesViewModel
            {
                Vehicles = paginatedItems.Select(MapToViewModel).ToList(),
                PageNumber = page,
                ElementsCount = totalItems,
                ItemsPerPage = pageSize
            };

            return View(viewModel);
        }

        public async Task<IActionResult> SearchByPrice(int minPrice, int maxPrice, int page = 1)
        {
            List<JsonVehicle> items = await LoadJsonVehiclesAsync();

            var filteredItems = items.Where(v => v.Price >= minPrice && v.Price <= maxPrice).ToList();
            var sortedItems = filteredItems.OrderBy(v => v.Price).ToList();

            var viewModel = CreateVehiclesViewModel(sortedItems, page);
            return View("Index", viewModel);
        }

        private async Task<List<JsonVehicle>> LoadJsonVehiclesAsync()
        {
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Importer", "CarsImporter.json");
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = await r.ReadToEndAsync();
                return JsonConvert.DeserializeObject<List<JsonVehicle>>(json);
            }
        }

        private VehicleInputViewModel MapToInputViewModel(JsonVehicle jsonVehicle)
        {
            return new VehicleInputViewModel
            {
                Brand = jsonVehicle.Brand,
                Model = jsonVehicle.Model,
                Price = jsonVehicle.Price,
                City = jsonVehicle.City,
                Color = jsonVehicle.Color,
                Run = jsonVehicle.Run,
                EngineType = jsonVehicle.EngineType,
                HorsePower = jsonVehicle.HorsePower,
                EngineVolume = jsonVehicle.EngineVolume,
                Country = jsonVehicle.Country,
                ZipCode = jsonVehicle.Zipcode,
                HasABS = jsonVehicle.HasABS,
                HasAllWheelDriveSystem = jsonVehicle.HasAllWheelDriveSystem,
                HasCentralLock = jsonVehicle.HasCentralLock,
                HasClimatronic = jsonVehicle.HasClimatronic,
                HasCruiseControl = jsonVehicle.HasCruiseControl,
                HasDVD = jsonVehicle.HasDVD,
                HasElectricWindows = jsonVehicle.HasElectricWindows,
                HasParkAssist = jsonVehicle.HasParkAssist,
                HasRadioBluetooth = jsonVehicle.HasRadioBluetooth,
                HasStabilityControl = jsonVehicle.HasStabilityControl,
                VehicleType = jsonVehicle.VehicleType,
                AdvertDate = jsonVehicle.AdvertDate 
            };
        }

        private VehiclesViewModel CreateVehiclesViewModel(List<JsonVehicle> items, int page)
        {
            int pageSize = 10;
            int totalItems = items.Count;

            var paginatedItems = items
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new VehiclesViewModel
            {
                Vehicles = paginatedItems.Select(MapToViewModel).ToList(),
                PageNumber = page,
                ElementsCount = totalItems,
                ItemsPerPage = pageSize
            };

            return viewModel;
        }

        private VehicleViewModel MapToViewModel(JsonVehicle jsonVehicle)
        {
            return new VehicleViewModel
            {
                Brand = jsonVehicle.Brand,
                Model = jsonVehicle.Model,
                Price = jsonVehicle.Price,
                City = jsonVehicle.City,
                Color = jsonVehicle.Color,
                Run = jsonVehicle.Run,
                Engine = jsonVehicle.EngineType,
                AdvertDate = jsonVehicle.AdvertDate,
                Tags = new List<string>
                {
                    jsonVehicle.HasStabilityControl ? "Stability Control" : "",
                    jsonVehicle.HasDVD ? "DVD" : "",
                    jsonVehicle.HasAllWheelDriveSystem ? "All Wheel Drive" : "",
                    jsonVehicle.HasABS ? "ABS" : "",
                    jsonVehicle.HasClimatronic ? "Climatronic" : "",
                    jsonVehicle.HasCruiseControl ? "Cruise Control" : "",
                    jsonVehicle.HasParkAssist ? "Park Assist" : "",
                    jsonVehicle.HasRadioBluetooth ? "Radio Bluetooth" : "",
                    jsonVehicle.HasCentralLock ? "Central Lock" : "",
                    jsonVehicle.HasElectricWindows ? "Electric Windows" : ""
                }.Where(tag => !string.IsNullOrEmpty(tag)).ToList()
            };
        }
    }
}
