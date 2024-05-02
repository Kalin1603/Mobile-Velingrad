using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile_Velingrad.Data.Models
{
    public class Vehicle
    {
        public Vehicle()
        {
            this.Tags = new HashSet<TagCars>();
            this.AdvertDate = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime AdvertDate { get; set; }

        public int? Run { get; set; }

        [ForeignKey(nameof(Model))]
        public int ModelId { get; set; }

        public virtual Model Model { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        [ForeignKey(nameof(Color))]
        public int ColorId { get; set; }

        public virtual Color Color { get; set; }

        [ForeignKey(nameof(VehicleType))]
        public int VehicleTypeId { get; set; }

        public virtual VehicleType VehicleType { get; set; }

        [ForeignKey(nameof(Engine))]
        public int EngineId { get; set; }

        public virtual Engine Engine { get; set; }

        [ForeignKey(nameof(ExtrasPackage))]
        public int ExtrasPackageId { get; set; }

        public virtual ExtrasPackage ExtrasPackage { get; set; }

        public virtual ICollection<TagCars> Tags { get; set; }
    }
}
