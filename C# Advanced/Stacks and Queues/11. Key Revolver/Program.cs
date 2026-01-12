using System;
using System.Collections.Generic;
using System.Linq;

namespace _11._Key_Revolver;

internal class Program
{
    static void Main(string[] args)
    {
        int bulletPrice = int.Parse(Console.ReadLine());
        int gunBarrelSize = int.Parse(Console.ReadLine());
        Stack<int> bullets = new Stack<int>(Console.ReadLine().Split(" ").Select(int.Parse).ToArray());
        Queue<int> locks = new Queue<int>(Console.ReadLine().Split(" ").Select(int.Parse).ToArray());
        int intelligenceValue = int.Parse(Console.ReadLine());

        int reloadCounter = 0;

        while (locks.Count > 0)
        {
            int bullet = bullets.Pop();

            int currentLock = locks.Peek();

            if (bullet <= currentLock)
            {
                Console.WriteLine("Bang!");
                locks.Dequeue();
            }
            else
            {
                Console.WriteLine("Ping!");
            }
            
            intelligenceValue -= bulletPrice;

            if (bullets.Count == 0)
            {
                break;
            }

            if (++reloadCounter == gunBarrelSize)
            {
                Console.WriteLine("Reloading!");
                reloadCounter = 0;
            }
        }

        if (!locks.Any())
        {
            Console.WriteLine($"{bullets.Count} bullets left. Earned ${intelligenceValue}");
        }
        else
        {
            Console.WriteLine($"Couldn't get through. Locks left: {locks.Count}");
        }
    }
}
