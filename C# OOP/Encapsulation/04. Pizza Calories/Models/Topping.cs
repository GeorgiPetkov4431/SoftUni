using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories.Models
{
    public class Topping
    {
        private const double BaseCaloriesPerGram = 2.0;

        private string type;
        private double weight;

        private Dictionary<string, double> toppingTypeCalories;

        public Topping(string toppingType, double weight)
        {
            toppingTypeCalories = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
            {
                { "meat", 1.2 },
                { "veggies", 0.8 },
                { "cheese", 1.1 },
                { "sauce", 0.9 }
            };

            this.Type = toppingType;
            this.Weight = weight;
        }

        public string Type
        {
            get { return this.type; }
            private set
            {
                if (!toppingTypeCalories.ContainsKey(value))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }

                this.type = value;
            }
        }

        public double Weight
        {
            get { return this.weight; }
            set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{this.Type} weight should be in the range [1..50].");
                }

                this.weight = value;
            }
        }

        public double Calories
        {
            get
            {
                return Weight * BaseCaloriesPerGram * toppingTypeCalories[Type];
            }
        }
    }
}
