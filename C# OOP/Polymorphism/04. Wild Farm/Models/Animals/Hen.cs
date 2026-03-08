using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models.Animals
{
    public class Hen : Bird
    {
        private const double DefaultWeightMultiplier = 0.35;

        public Hen(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }

        protected override double WeightMultiplier { get { return DefaultWeightMultiplier; } }

        public override string ProduceSound()
        {
            return "Cluck";
        }
    }
}
