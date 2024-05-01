namespace Mobile_Velingrad.Data.Models
{
    public class Engine
    {
        public int Id { get; set; }

        public double Volume { get; set; }

        public int HorsePower { get; set; }

        public virtual EngineType EngineType { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
