using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Basic_Queue_Operations;

internal class Program
{
    static void Main(string[] args)
    {
        int[] values = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int n = values[0];
        int s = values[1];
        int x = values[2];

        int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
        Queue<int> queue = new Queue<int>();

        for (int i = 0; i < n;  i++)
        {
            queue.Enqueue(nums[i]);
        }

        for (int i = 0; i < s; i++)
        {
            queue.Dequeue();
        }

        if (!queue.Any())
        {
            Console.WriteLine(0);
        }
        else if (queue.Contains(x))
        {
            Console.WriteLine("true");
        }
        else
        {
            Console.WriteLine(queue.Min());
        }
    }
}
