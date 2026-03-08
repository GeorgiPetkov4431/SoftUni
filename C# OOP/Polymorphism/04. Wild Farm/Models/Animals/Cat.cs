using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models.Animals
{
    public class Cat : Feline
    {
        private const double DefaultWeightMultiplier = 0.30;

        public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }

        protected override double WeightMultiplier { get { return DefaultWeightMultiplier; } }

        public override string ProduceSound()
        {
            return "Meow";
        }

        public override void Eat(IFood food)
        {
            if (food is not Meat and not Vegetable)
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.GetType().Name}!");
            }

            base.Eat(food);
        }
    }
}
