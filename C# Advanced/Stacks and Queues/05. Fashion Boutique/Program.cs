using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Fashion_Boutique;

internal class Program
{
    static void Main(string[] args)
    {
        Stack<int> clothes = new Stack<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());
        int rackCapacity = int.Parse(Console.ReadLine());
        int currentRack = 0;
        int totalRacks = 1;

        while (clothes.Any())
        {
            int currentItem = clothes.Peek();
            if (currentItem + currentRack <=  rackCapacity)
            {
                currentRack += clothes.Pop();
            }
            else
            {
                currentRack = 0;
                totalRacks++;
            }
        }

        Console.WriteLine(totalRacks);
    }
}
