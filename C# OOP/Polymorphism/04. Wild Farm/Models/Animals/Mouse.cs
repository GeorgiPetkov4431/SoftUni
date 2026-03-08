using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models.Animals
{
    public class Mouse : Mammal
    {
        private const double DefaultWeightMultiplier = 0.10;

        public Mouse(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        protected override double WeightMultiplier { get { return DefaultWeightMultiplier; } }

        public override string ProduceSound()
        {
            return "Squeak";
        }

        public override void Eat(IFood food)
        {
            if (food is not Vegetable and not Fruit)
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.GetType().Name}!");
            }

            base.Eat(food);
        }
    }
}
