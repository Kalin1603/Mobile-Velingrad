namespace Mobile_Velingrad.Data.Models
{
    public class EngineType
    {
        public EngineType()
        {
            this.Engines = new HashSet<Engine>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Engine> Engines { get; set; }
    }
}
