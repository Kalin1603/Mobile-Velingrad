namespace Mobile_Velingrad.Data.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime AdvertDate { get; set; }

        public int? Run { get; set; }

        public int ModelId { get; set; }

        public virtual Model Model { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public int ColorId { get; set; }

        public virtual Color Color { get; set; }

        public int VehicleTypeId { get; set; }

        public virtual VehicleType VehicleType { get; set; }

        public int EngineId { get; set; }

        public virtual Engine Engine { get; set; }

        public int ExtrasPackageId { get; set; }

        public virtual ExtrasPackage ExtrasPackage { get; set; }

        public virtual ICollection<TagCars> Tags { get; set; }
    }
}
