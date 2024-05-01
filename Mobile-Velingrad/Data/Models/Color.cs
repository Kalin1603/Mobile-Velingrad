namespace Mobile_Velingrad.Data.Models
{
    public class Color
    {
        public Color()
        {
            this.Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ColorCode { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
