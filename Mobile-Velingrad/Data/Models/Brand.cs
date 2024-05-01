using System.ComponentModel.DataAnnotations;

namespace Mobile_Velingrad.Data.Models
{
    public class Brand
    {
        public Brand()
        {
            this.Models = new HashSet<Model>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Model> Models { get; set; }

    }
}
