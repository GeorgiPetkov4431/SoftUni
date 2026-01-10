using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Fast_Food;

internal class Program
{
    static void Main(string[] args)
    {
        short foodQuantity = short.Parse(Console.ReadLine());
        Queue<short> orders = new Queue<short>(Console.ReadLine().Split().Select(short.Parse).ToArray());

        Console.WriteLine(orders.Max());

        while (orders.Count > 0)
        {
            if (orders.Peek() <= foodQuantity)
            {
                foodQuantity -= orders.Dequeue();
            }
            else break;
        }

        if (!orders.Any())
        {
            Console.WriteLine("Orders complete");
        } else
        {
            Console.WriteLine($"Orders left: {string.Join(" ", orders)}");
        }
    }
}
