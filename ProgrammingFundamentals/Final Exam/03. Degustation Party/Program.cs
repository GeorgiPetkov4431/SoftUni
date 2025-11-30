using System;
using System.Collections.Generic;

namespace _03._Degustation_Party;

internal class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, List<string>> guests = new Dictionary<string, List<string>>();
        int unlikedMeals = 0;

        string command;
        while ((command = Console.ReadLine()) != "Stop")
        {
            string[] commandArgs = command.Split("-", StringSplitOptions.RemoveEmptyEntries);
            string guestName = commandArgs[1];
            string meal = commandArgs[2];

            switch (commandArgs[0])
            {
                case "Like":
                    if (!guests.ContainsKey(guestName))
                    {
                        guests.Add(guestName, new List<string>());
                    }
                    if (!guests[guestName].Contains(meal)) guests[guestName].Add(meal);
                    break;
                case "Dislike":
                    if (!guests.ContainsKey(guestName))
                    {
                        Console.WriteLine($"{guestName} is not at the party.");
                        continue;
                    }
                    int mealIndex = guests[guestName].IndexOf(meal);
                    if (mealIndex == -1)
                    {
                        Console.WriteLine($"{guestName} doesn't have the {meal} in his/her collection.");
                        continue;
                    }
                    guests[guestName].Remove(meal);
                    Console.WriteLine($"{guestName} doesn't like the {meal}.");
                    unlikedMeals++;
                    break;
            }
        }

        foreach (KeyValuePair<string, List<string>> guest in guests)
        {
            Console.WriteLine($"{guest.Key}: {string.Join(", ", guest.Value)}");
        }
        Console.WriteLine($"Unliked meals: {unlikedMeals}");
    }
}
