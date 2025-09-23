using System;

namespace _09._Spice_Must_Flow;

internal class Program
{
    static void Main(string[] args)
    {
        uint startingYield = uint.Parse(Console.ReadLine());

        long totalYield = 0;
        int totalDays = 0;

        while (startingYield >= 100) {
            totalYield += (startingYield - 26);
            totalDays++;
            startingYield -= 10;
        }
        totalYield = Math.Max(0, totalYield - 26);

        Console.WriteLine(totalDays);
        Console.WriteLine(totalYield);
    }
}
