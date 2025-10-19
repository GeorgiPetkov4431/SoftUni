using System;
using System.Linq;
using System.Collections.Generic;

namespace _03._Moving_Target;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> targets = Console.ReadLine().Split().Select(int.Parse).ToList();

        string command;

        while ((command = Console.ReadLine()) != "End")
        {
            string[] commandArgs = command.Split();
            switch (commandArgs[0])
            {
                case "Shoot":
                    int shootIndex = int.Parse(commandArgs[1]);
                    int shootPower = int.Parse(commandArgs[2]);
                    Shoot(targets, shootIndex, shootPower);
                    break;
                case "Add":
                    int addIndex = int.Parse(commandArgs[1]);
                    int value = int.Parse(commandArgs[2]);
                    Add(targets, addIndex, value);
                    break;
                case "Strike":
                    int strikeIndex = int.Parse(commandArgs[1]);
                    int strikeRadius = int.Parse(commandArgs[2]);
                    Strike(targets, strikeIndex, strikeRadius);
                    break;
            }
        }

        Console.WriteLine(string.Join('|', targets));
    }

    static void Shoot(List<int> targets, int index, int power)
    {
        if (!IsValidIndex(targets.Count, index)) return;
        targets[index] -= power;
        if (targets[index] <= 0) targets.RemoveAt(index);
    }

    static void Add(List<int> targets, int index, int value)
    {
        if (!IsValidIndex(targets.Count, index))
        {
            Console.WriteLine("Invalid placement!");
            return;
        }
        targets.Insert(index, value);
    }

    static void Strike(List<int> targets, int index, int radius)
    {
        if (!IsValidIndex(targets.Count, index)) return;
        if (index - radius < 0 || index + radius >= targets.Count)
        {
            Console.WriteLine("Strike missed!");
            return;
        }
        int strikeRange = radius * 2 + 1;
        targets.RemoveRange(index - radius, strikeRange);
    }

    static bool IsValidIndex(int listLength, int index)
    {
        if (index >= 0 && index < listLength) return true;
        return false;
    }
}
