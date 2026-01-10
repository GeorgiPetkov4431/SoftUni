using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Basic_Stack_Operations;

internal class Program
{
    static void Main(string[] args)
    {
        int[] values = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int n = values[0];
        int s = values[1];
        int x = values[2];

        int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
        Stack<int> stack = new Stack<int>();

        for (int i = 0; i < n; i++)
        {
            stack.Push(nums[i]);
        }

        for (int i = 0; i < s ; i++)
        {
            stack.Pop();
        }

        if (!stack.Any())
        {
            Console.WriteLine(0);
        }
        else if (stack.Contains(x))
        {
            Console.WriteLine("true");
        }
        else
        {
            Console.WriteLine(stack.Min());
        }
    }
}
