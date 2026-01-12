using System;
using System.Collections.Generic;
using System.Linq;

namespace _12._Cups_and_Bottles;

internal class Program
{
    static void Main(string[] args)
    {
        Queue<int> cups = new Queue<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());
        Stack<int> bottles = new Stack<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());

        int wastedLiters = 0;

        while (cups.Count > 0 && bottles.Count > 0)
        {
            int currentCup = cups.Peek();
            
            while (bottles.Any())
            {
                int currentBottle = bottles.Pop();

                currentCup -= currentBottle;

                if (currentCup <= 0)
                {
                    cups.Dequeue();

                    wastedLiters -= currentCup;

                    break;
                }
            }
        }

        if (!bottles.Any())
        {
            Console.WriteLine($"Cups: {string.Join(" ", cups)}");
        }
        if (!cups.Any())
        {
            Console.WriteLine($"Bottles: {string.Join(" ", bottles)}");
        }
        Console.WriteLine($"Wasted litters of water: {wastedLiters}");
    }
}
