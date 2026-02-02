namespace RawData
{
    public class Cargo
    {
        public Cargo(string type, double weight)
        {
            this.Type = type;
            this.Weight = weight;
        }

        public string Type { get; set; }
        public double Weight { get; set; }
    }
}