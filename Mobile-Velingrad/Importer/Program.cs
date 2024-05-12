using Mobile_Velingrad.Data;
using Mobile_Velingrad.Services;
using Mobile_Velingrad.ViewModels;
using System.Text.Json;

namespace Mobile_Velingrad.Importer
{
    public class Program
    {
        public static async Task Main()
        {
            try
            {
                var json = await File.ReadAllTextAsync("CarsImporter.json");
                var vehicles = JsonSerializer.Deserialize<IEnumerable<JsonVehicle>>(json);

                if (vehicles == null)
                {
                    await Console.Out.WriteLineAsync("No vehicles found.");
                    return;
                }

                var context = new AppDbContext();
                var service = new VehicleService(context);

                foreach (var vehicle in vehicles)
                {
                    try
                    {
                        await service.CreateAsync(new VehicleInputViewModel()
                        {
                            Brand = vehicle.Brand,
                            Model = vehicle.Model,
                            VehicleType = vehicle.VehicleType,
                            City = vehicle.City,
                            Country = vehicle.Country,
                            Color = vehicle.Color,
                            EngineType = vehicle.EngineType,
                            EngineVolume = vehicle.EngineVolume,
                            Price = vehicle.Price,
                            Run = vehicle.Run,
                            ZipCode = vehicle.ZipCode,
                            HorsePower = vehicle.HorsePower,
                            HasABS = vehicle.HasABS,
                            HasAllWheelDriveSystem = vehicle.HasAllWheelDriveSystem,
                            HasCentralLock = vehicle.HasCentralLock,
                            HasClimatronic = vehicle.HasClimatronic,
                            HasCruiseControl = vehicle.HasCruiseControl,
                            HasDVD = vehicle.HasDVD,
                            HasElectricWindows = vehicle.HasElectricWindows,
                            HasParkAssist = vehicle.HasParkAssist,
                            HasRadioBluetooth = vehicle.HasRadioBluetooth,
                            HasStabilityControl = vehicle.HasStabilityControl,
                        });
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Error processing vehicle: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error reading or deserializing file: {ex.Message}");
            }
        }
    }
}
