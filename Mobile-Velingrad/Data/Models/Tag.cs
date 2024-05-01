namespace Mobile_Velingrad.Data.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Tags = new HashSet<TagCars>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<TagCars> Tags { get; set; }
    }
}
