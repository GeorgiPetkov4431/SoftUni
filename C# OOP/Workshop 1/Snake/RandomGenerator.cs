using Snake.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random random;

        public RandomGenerator()
        {
            this.random = new Random();
        }

        public int NextNumber(int min = 0, int max = int.MaxValue)
        {
            return random.Next(min, max);
        }
    }
}
