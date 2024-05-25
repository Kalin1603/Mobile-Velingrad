using Mobile_Velingrad.Importer;
using Mobile_Velingrad.ViewModels;

namespace Mobile_Velingrad.Services
{
    public interface IVehicleService
    {
        Task CreateAsync(VehicleInputViewModel inputModel);

        Task<VehiclesViewModel> GetVehiclesAsync(int pageNumber = 1);

        Task<TopVehicleViewModel> GetTopExpensiveVehiclesAsync();

        Task<TopVehicleViewModel> SearchByPriceAsync();

        Task<TopVehicleViewModel> GetLastAddedVehiclesAsync();

        Task<SearchVehiclesViewModel> SearchByPriceAsync(int minPrice, int maxPrice, int pageNumber);

        Task<bool> DeleteVehicleAsync(int id);
    }
}
