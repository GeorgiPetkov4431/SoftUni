using System;
using System.Collections.Generic;
using System.Linq;

namespace _10._Party_Reservation_Filter_Module;

internal class Program
{
    static void Main(string[] args)
    {
        string[] names = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Dictionary<string, Predicate<string>> filters = new Dictionary<string, Predicate<string>>();

        string command;
        
        while ((command = Console.ReadLine()) != "Print")
        {
            string[] commandArgs = command.Split(';', StringSplitOptions.RemoveEmptyEntries);

            string operation = commandArgs[0];
            string filterType = commandArgs[1];
            string filterValue = commandArgs[2];

            string filterCondition = $"{filterType},{filterValue}";

            Predicate<string> filter = null;

            switch (filterType)
            {
                case "Starts with":
                    filter = name => name.StartsWith(filterValue);
                    break;

                case "Ends with":
                    filter = name => name.EndsWith(filterValue);
                    break;

                case "Length":
                    filter = name => name.Length == int.Parse(filterValue);
                    break;

                case "Contains":
                    filter = name => name.Contains(filterValue);
                    break;
            }

            if (filter != null)
            {
                switch (operation)
                {
                    case "Add filter":
                        if (!filters.ContainsKey(filterCondition))
                        {
                            filters.Add(filterCondition, filter);
                        }
                        break;

                    case "Remove filter":
                        if (filters.ContainsKey(filterCondition))
                        {
                            filters.Remove(filterCondition);
                        }
                        break;
                }
            }
        }

        foreach (var filter in filters)
        {
            names = names.Where(name => !filter.Value(name)).ToArray();
        }

        Console.WriteLine(string.Join(" ", names));
    }
}
