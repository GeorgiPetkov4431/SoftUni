using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Predicate_for_Names;

internal class Program
{
    static void Main(string[] args)
    {
        int limit = int.Parse(Console.ReadLine());
        string[] names = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Predicate<int> predicate = len => len <= limit;
        string[] result = names.Where(n => predicate(n.Length)).ToArray();

        Console.WriteLine(string.Join("\n", result));
    }
}
