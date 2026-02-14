using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Energy_Drinks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stack<int> mgOfCaffeine = new Stack<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            Queue<int> energyDrinks = new Queue<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            int totalCaffeine = 0;

            while (mgOfCaffeine.Any() && energyDrinks.Any())
            {
                int caffeine = mgOfCaffeine.Pop();
                int energyDrink = energyDrinks.Dequeue();

                int currentCaffeine = caffeine * energyDrink;

                if (currentCaffeine + totalCaffeine <= 300)
                {
                    totalCaffeine += currentCaffeine;
                }
                else
                {
                    energyDrinks.Enqueue(energyDrink);
                    totalCaffeine = int.Max(0, totalCaffeine - 30);
                }
            }

            if (energyDrinks.Any())
            {
                Console.WriteLine($"Drinks left: {string.Join(", ", energyDrinks)}");
            }
            else
            {
                Console.WriteLine("At least Stamat wasn't exceeding the maximum caffeine.");
            }

            Console.WriteLine($"Stamat is going to sleep with {totalCaffeine} mg caffeine.");
        }
    }
}
