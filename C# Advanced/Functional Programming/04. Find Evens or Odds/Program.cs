using System;
using System.Linq;

namespace _04._Find_Evens_or_Odds;

internal class Program
{
    static void Main(string[] args)
    {
        Func<int, bool> predicate;

        int[] range = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        int lowerBound = range[0];
        int upperBound = range[1];

        string command = Console.ReadLine();

        if (command == "even")
        {
            predicate = x => x % 2 == 0;
        }
        else
        {
            predicate = x => Math.Abs(x) % 2 == 1;
        }

        for (int i = lowerBound; i <= upperBound; i++)
        {
            if (predicate(i)) Console.Write($"{i} ");
        }
    }
}
