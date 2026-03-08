using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models.Animals
{
    public class Dog : Mammal
    {
        private const double DefaultWeightMultiplier = 0.40;

        public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }
        
        protected override double WeightMultiplier { get { return DefaultWeightMultiplier; } }

        public override string ProduceSound()
        {
            return "Woof!";
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
