using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories.Models
{
    public class Pizza
    {
        private string name;
        private List<Topping> toppings;

        public Pizza(string name, Dough dough)
        {
            this.toppings = new List<Topping>();

            this.Name = name;
            this.Dough = dough;
        }

        public Dough Dough { get; set; }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (value.Length < 1 || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }

                this.name = value;
            }
        }

        public int ToppingNumber
        {
            get { return this.toppings.Count; }
        }

        public double Calories
        {
            get
            {
                return Dough.Calories + toppings.Sum(t => t.Calories);
            }
        }

        public void AddTopping(Topping topping)
        {
            if (this.toppings.Count == 10)
            {
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            }

            this.toppings.Add(topping);
        }

        public override string ToString()
        {
            return $"{this.Name} - {this.Calories:F2} Calories.";
        }
    }
}
