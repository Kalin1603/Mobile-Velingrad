using System.ComponentModel.DataAnnotations;

namespace Mobile_Velingrad.Data.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Tags = new HashSet<TagCars>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<TagCars> Tags { get; set; }
    }
}
