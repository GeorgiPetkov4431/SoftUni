using System;
using System.Collections.Generic;
using System.Linq;

namespace _08._List_of_Predicates;

internal class Program
{
    static void Main(string[] args)
    {
        int upperBound = int.Parse(Console.ReadLine());
        int[] nums = Enumerable.Range(1, upperBound).ToArray();

        int[] divisors = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        HashSet<Predicate<int>> filters = new HashSet<Predicate<int>>();

        foreach (int divisor in divisors)
        {
            filters.Add(n => n % divisor == 0);
        }

        foreach (var filter in filters)
        {
            nums = nums.Where(n => filter(n)).ToArray();
        }

        if (nums.Length != 0) Console.WriteLine(string.Join(" ", nums));
    }
}
