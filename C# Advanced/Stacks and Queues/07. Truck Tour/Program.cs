using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Truck_Tour;

internal class Program
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        Queue<int[]> stops = new Queue<int[]>();

        for (int i = 0; i < N; i++)
        {
            stops.Enqueue(Console.ReadLine().Split().Select(int.Parse).ToArray());
        }

        int stopNum = 0;

        while (true)
        {
            int fuel = 0;

            foreach (int[] stop in stops)
            {
                int currentFuel = stop[0];
                int distance = stop[1];

                fuel += currentFuel - distance;
                if (fuel < 0)
                {
                    stopNum++;
                    stops.Enqueue(stops.Dequeue());
                    break;
                }
            }

            if (fuel > 0)
            {
                Console.WriteLine(stopNum);
                break;
            }
        }
    }
}
