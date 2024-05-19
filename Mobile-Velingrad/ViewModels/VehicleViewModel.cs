namespace Mobile_Velingrad.ViewModels
{
    public class VehicleViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public decimal Price { get; set; }

        public DateTime AdvertDate { get; set; }

        public int? Run { get; set; }

        public string Model { get; set; }

        public string City { get; set; }

        public string Color { get; set; }

        public string Engine { get; set; }

        public int VehicleTypeId { get; set; }

        public int ExtrasPackageId { get; set; }

        public List<string> Tags { get; set; }
    }
}
