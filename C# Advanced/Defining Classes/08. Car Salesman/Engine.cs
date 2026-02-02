using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSalesman
{
    public class Engine
    {
        public Engine(string model, int power, int? displacement = null, string efficiency = null)
        {
            Model = model;
            Power = power;
            Displacement = displacement;
            Efficiency = efficiency;
        }

        public string Model { get; set; }
        public int Power { get; set; }
        public int? Displacement { get; set; }
        public string Efficiency { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"  {Model}:");
            sb.AppendLine($"    Power: {Power}");
            if (Displacement != null)
            {
                sb.AppendLine($"    Displacement: {Displacement}");
            }
            else
            {
                sb.AppendLine("    Displacement: n/a");
            }

            if (Efficiency != null)
            {
                sb.AppendLine($"    Efficiency: {Efficiency}");
            }
            else
            {
                sb.AppendLine("    Efficiency: n/a");
            }

                return sb.ToString();
        }
    }
}
