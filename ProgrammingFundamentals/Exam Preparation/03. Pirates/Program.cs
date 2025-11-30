using System;
using System.Collections.Generic;

namespace _03._Pirates;

internal class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, City> cities = new Dictionary<string, City>();

        string command;

        while ((command = Console.ReadLine()) != "Sail")
        {
            string[] commandArgs = command.Split("||", StringSplitOptions.RemoveEmptyEntries);
            string cityName = commandArgs[0];
            int population = int.Parse(commandArgs[1]);
            int gold = int.Parse(commandArgs[2]);

            if (!cities.ContainsKey(cityName))
            {
                cities.Add(cityName, new City(cityName, population, gold));
            }
            else
            {
                cities[cityName].Population += population;
                cities[cityName].Gold += gold;
            }
        }

        while ((command = Console.ReadLine()) != "End")
        {
            string[] commandArgs = command.Split("=>", StringSplitOptions.RemoveEmptyEntries);

            string cityName = commandArgs[1];

            switch (commandArgs[0])
            {
                case "Plunder":
                    int plunderPeople = int.Parse(commandArgs[2]);
                    int plunderGold = int.Parse(commandArgs[3]);

                    cities[cityName].Plunder(plunderPeople, plunderGold);
                    Console.WriteLine($"{cityName} plundered! {plunderGold} gold stolen, {plunderPeople} citizens killed.");
                    if (cities[cityName].Gold <= 0 || cities[cityName].Population <= 0)
                    {
                        cities.Remove(cityName);
                        Console.WriteLine($"{cityName} has been wiped off the map!");
                    }
                    break;

                case "Prosper":
                    int prosperGold = int.Parse(commandArgs[2]);
                    if (prosperGold >= 0)
                    {
                        cities[cityName].Prosper(prosperGold);
                        Console.WriteLine($"{prosperGold} gold added to the city treasury. {cityName} now has {cities[cityName].Gold} gold.");
                    }
                    else
                    {
                        Console.WriteLine("Gold added cannot be a negative number!");
                    }

                    break;
            }
        }

        if (cities.Count > 0)
        {
            Console.WriteLine($"Ahoy, Captain! There are {cities.Count} wealthy settlements to go to:");
            foreach (KeyValuePair<string, City> city in cities)
            {
                Console.WriteLine(city.Value);
            }
        }
        else Console.WriteLine("Ahoy, Captain! All targets have been plundered and destroyed!");
    }
}

class City
{
    public string Name { get; set; }
    public int Population { get; set; }
    public int Gold { get; set; }

    public City(string name, int population, int gold)
    {
        Name = name;
        Population = population;
        Gold = gold;
    }

    public void Plunder(int people, int gold)
    {
        Population -= people;
        Gold -= gold;
    }

    public void Prosper(int gold)
    {
        Gold += gold;
    }

    public override string ToString()
    {
        return $"{Name} -> Population: {Population} citizens, Gold: {Gold} kg";
    }
}
