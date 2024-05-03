namespace Mobile_Velingrad.ViewModels
{
    public class VehiclesViewModel : PagingViewModel
    {
        public ICollection<VehicleViewModel> Vehicles { get; set; }
    }
}
