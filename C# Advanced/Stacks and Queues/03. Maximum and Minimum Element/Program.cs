using System;
using System.Linq;
using System.Collections.Generic;

namespace _03._Maximum_and_Minimum_Element;

internal class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());

        Stack<int> stack = new Stack<int>();

        for (int i = 0; i < n; i++)
        {
            string query = Console.ReadLine();

            if (query.StartsWith("1"))
            {
                int number = int.Parse(query.Substring(2));
                stack.Push(number);
            }
            else if (query == "2")
            {
                stack.Pop();
            }
            else if (query == "3" && stack.Any())
            {
                Console.WriteLine(stack.Max());
            }
            else if (query == "4" && stack.Any())
            {
                Console.WriteLine(stack.Min());
            }
        }

        Console.WriteLine(string.Join(", ", stack));
    }
}
