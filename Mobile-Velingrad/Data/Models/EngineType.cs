using System.ComponentModel.DataAnnotations;

namespace Mobile_Velingrad.Data.Models
{
    public class EngineType
    {
        public EngineType()
        {
            this.Engines = new HashSet<Engine>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<Engine> Engines { get; set; }
    }
}
