using System;
using System.Collections.Generic;

namespace _02._A_Miner_Task;

internal class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, uint> resources = new();

        string command;

        while ((command = Console.ReadLine()) != "stop")
        {
            uint quantity = uint.Parse(Console.ReadLine());

            if (!resources.ContainsKey(command))
            {
                resources.Add(command, 0);
            }

            resources[command] += quantity;
        }

        foreach (KeyValuePair<string, uint> resource in resources)
        {
            Console.WriteLine($"{resource.Key} -> {resource.Value}");
        }
    }
}
