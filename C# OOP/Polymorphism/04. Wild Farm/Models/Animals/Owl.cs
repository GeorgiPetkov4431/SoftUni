using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models.Animals
{
    public class Owl : Bird
    {
        private const double DefaultWeightMultiplier = 0.25;

        public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }

        protected override double WeightMultiplier { get { return DefaultWeightMultiplier; } }

        public override string ProduceSound()
        {
            return "Hoot Hoot";
        }

        public override void Eat(IFood food)
        {
            if (food is not Meat)
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.GetType().Name}!");
            }

            base.Eat(food);
        }
    }
}
