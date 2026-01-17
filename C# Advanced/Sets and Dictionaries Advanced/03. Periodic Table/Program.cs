using System;
using System.Collections.Generic;

namespace _03._Periodic_Table;

internal class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());

        SortedSet<string> elements = new SortedSet<string>();

        for (int i = 0; i < n; i++)
        {
            string[] newElements = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string element in newElements)
            {
                elements.Add(element);
            }
        }

        foreach (string element in elements)
        {
            Console.Write($"{element} ");
        }
    }
}
