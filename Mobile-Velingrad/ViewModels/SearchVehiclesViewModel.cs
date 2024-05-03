namespace Mobile_Velingrad.ViewModels
{
    public class SearchVehiclesViewModel
    {
        public ICollection<VehicleViewModel> Vehicles { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }
    }
}
