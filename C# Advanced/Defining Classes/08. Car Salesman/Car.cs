using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSalesman
{
    public class Car
    {
        public Car(string model, Engine engine, int? weight = null, string color = null)
        {
            Model = model;
            Engine = engine;
            Weight = weight;
            Color = color;
        }

        public string Model { get; set; }
        public Engine Engine {  get; set; }
        public int? Weight { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Model}:");
            sb.Append(Engine.ToString());
            if (Weight != null)
            {
                sb.AppendLine($"  Weight: {Weight}");
            }
            else
            {
                sb.AppendLine("  Weight: n/a");
            }
            if (Color != null)
            {
                sb.Append($"  Color: {Color}");
            }
            else
            {
                sb.Append("  Color: n/a");
            }

            return sb.ToString();
        }
    }
}
