using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile_Velingrad.Data.Models
{
    public class Model
    {
        public Model()
        {
            this.Vehicles = new HashSet<Vehicle>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
