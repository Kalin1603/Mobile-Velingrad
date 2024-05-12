using Microsoft.AspNetCore.Mvc;
using Mobile_Velingrad.Services;

public class VehicleController : Controller
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<IActionResult> ImportVehicles()
    {
        try
        {
            await _vehicleService.ImportVehiclesFromFile("D:\\Mobile-Velingrad\\Mobile-Velingrad\\Importer\\CarsImporter.json");
            return Ok("Vehicles imported successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error during import: {ex.Message}");
        }
    }
}
