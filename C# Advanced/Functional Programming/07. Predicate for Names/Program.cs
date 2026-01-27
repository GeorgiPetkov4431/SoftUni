using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Predicate_for_Names;

internal class Program
{
    static void Main(string[] args)
    {
        int limit = int.Parse(Console.ReadLine());
        List<string> names = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

        Predicate<int> predicate = len => len <= limit;
        List<string> result = new List<string>();
        for (int i = 0; i < names.Count; i++)
        {
            if (predicate(names[i].Length)) result.Add(names[i]);
        }

        Console.WriteLine(string.Join("\n", result));
    }
}
