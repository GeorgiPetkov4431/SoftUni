using System;
using System.Linq;

namespace _05._Shoot_for_the_Win;

internal class Program
{
    static void Main(string[] args)
    {
        int[] targets = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int targetsShot = 0;

        string command;

        while ((command = Console.ReadLine()) != "End")
        {
            int targetIndex = int.Parse(command);
            if (targetIndex < 0 || targetIndex >= targets.Length) continue;
            else if (targets[targetIndex] == -1) continue;
                int targetValue = targets[targetIndex];
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] != -1)
                {
                    if (targets[i] > targetValue) targets[i] -= targetValue;
                    else targets[i] += targetValue;
                }
            }
            targetsShot++;
            targets[targetIndex] = -1;
        }

        Console.Write($"Shot targets: {targetsShot} -> ");
        Console.WriteLine(string.Join(' ', targets));
    }
}
