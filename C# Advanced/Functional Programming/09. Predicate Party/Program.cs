using System;
using System.Collections.Generic;
using System.Linq;

namespace _09._Predicate_Party;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> names = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        string command;

        while ((command = Console.ReadLine()) != "Party!")
        {
            string[] commandArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string operation = commandArgs[0];
            string criteria = commandArgs[1];

            Predicate<string> filter = null;

            switch (criteria)
            {
                case "StartsWith":
                    filter = name => name.StartsWith(commandArgs[2]);
                    break;

                case "EndsWith":
                    filter = name => name.EndsWith(commandArgs[2]);
                    break;

                case "Length":
                    filter = name => name.Length == int.Parse(commandArgs[2]);
                    break;
            }

            switch (operation)
            {
                case "Double":
                    for (int i = 0; i < names.Count; i++)
                    {
                        if (filter(names[i]))
                        {
                            names.Insert(i, names[i]);
                            i++;
                        }
                    }
                    break;

                case "Remove":
                    for (int i = 0; i < names.Count; i++)
                    {
                        if (filter(names[i]))
                        {
                            names.RemoveAt(i);
                            i--;
                        }
                    }
                    break;
            }
        }

        if (names.Any())
        {
            Console.WriteLine($"{string.Join(", ", names)} are going to the party!");
        }
        else
        {
            Console.WriteLine("Nobody is going to the party!");
        }
    }
}
