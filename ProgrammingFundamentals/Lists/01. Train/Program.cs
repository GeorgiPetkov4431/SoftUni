using System;
using System.Linq;
using System.Collections.Generic;

namespace _01._Train;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> wagons = Console.ReadLine().Split().Select(int.Parse).ToList();
        int maxWagonCapacity = int.Parse(Console.ReadLine());

        string command;

        while ((command = Console.ReadLine()) != "end")
        {
            string[] commandArgs = command.Split();
            if (commandArgs[0] == "Add") wagons = AddPassengers(wagons, int.Parse(commandArgs[1]));
            else
            {
                int passengers = int.Parse(commandArgs[0]);
                for (int i = 0; i < wagons.Count; i++)
                {
                    if (wagons[i] + passengers <= maxWagonCapacity)
                    {
                        wagons[i] += passengers;
                        break;
                    }
                }
            }
        }

        Console.WriteLine(string.Join(' ', wagons));
    }

    static List<int> AddPassengers(List<int> wagons, int passengers)
    {
        wagons.Add(passengers);
        return wagons;
    }
}
