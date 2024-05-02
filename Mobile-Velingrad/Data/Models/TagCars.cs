namespace Mobile_Velingrad.Data.Models
{
    public class TagCars
    {
        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
