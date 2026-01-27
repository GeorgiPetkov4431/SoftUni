using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Custom_Min_Function;

internal class Program
{
    static void Main(string[] args)
    {
        HashSet<int> values = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
        Func<HashSet<int>, int> function = values =>
        {
            int min = int.MaxValue;

            foreach (int i in values)
            {
                min = (i < min) ? i : min;
            }

            return min;
        };
        Console.WriteLine(function(values));
    }
}
