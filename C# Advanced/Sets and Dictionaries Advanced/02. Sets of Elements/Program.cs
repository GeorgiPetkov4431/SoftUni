using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Sets_of_Elements;

internal class Program
{
    static void Main(string[] args)
    {
        int[] sizes = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int n = sizes[0];
        int m = sizes[1];

        HashSet<int> nSet = new HashSet<int>();
        HashSet<int> mSet = new HashSet<int>();

        for (int i = 0; i < n; i++)
        {
            nSet.Add(int.Parse(Console.ReadLine()));
        }

        for (int i = 0; i < m; i++)
        {
            mSet.Add(int.Parse(Console.ReadLine()));
        }

        foreach (int num in nSet)
        {
            if (mSet.Contains(num))
            {
                Console.Write($"{num} ");
            }
        }
    }
}
