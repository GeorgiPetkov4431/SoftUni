using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Reverse_and_Exclude;

internal class Program
{
    static void Main(string[] args)
    {
        int[] nums = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        int divider = int.Parse(Console.ReadLine());

        Predicate<int> predicate = num => num % divider == 0;
        
        int[] result = nums.Where(n => !predicate(n)).Reverse().ToArray();
        Console.WriteLine(string.Join(" ", result));
    }
}
