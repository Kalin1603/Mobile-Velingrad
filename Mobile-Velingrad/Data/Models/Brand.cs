using System.ComponentModel.DataAnnotations;

namespace Mobile_Velingrad.Data.Models
{
    public class Brand
    {
        public Brand()
        {
            this.Models = new HashSet<Model>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Model> Models { get; set; }

    }
}
