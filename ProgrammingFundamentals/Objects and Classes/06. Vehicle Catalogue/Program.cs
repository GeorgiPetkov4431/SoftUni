using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Vehicle_Catalogue;

internal class Program
{
    static void Main(string[] args)
    {
        List<Vehicle> vehicles = new List<Vehicle>();

        string command;

        while ((command = Console.ReadLine()) != "End")
        {
            string[] commandArgs = command.Split();

            vehicles.Add(new Vehicle(commandArgs[0], commandArgs[1], commandArgs[2], decimal.Parse(commandArgs[3])));
        }

        while ((command = Console.ReadLine()) != "Close the Catalogue")
        {
            Vehicle found = vehicles.FirstOrDefault(v => v.Model == command);
            if (found != null)
            {
                Console.WriteLine(found);
            }
        }

        decimal averageHP = vehicles.Where(v => v.Type == "Car").Select(v => v.HorsePower).DefaultIfEmpty().Average();
        Console.WriteLine($"Cars have average horsepower of: {averageHP:F2}.");

        averageHP = vehicles.Where(v => v.Type == "Truck").Select(v => v.HorsePower).DefaultIfEmpty().Average();
        Console.WriteLine($"Trucks have average horsepower of: {averageHP:F2}.");
    }
}

class Vehicle
{
    public Vehicle(string type, string model, string color, decimal horsePower)
    {
        this.Type = type == "car" ? "Car" : "Truck";
        this.Model = model;
        this.Color = color;
        this.HorsePower = horsePower;
    }

    public string Type { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public decimal HorsePower { get; set; }

    public override string ToString()
    {
        return $"Type: {Type}\nModel: {Model}\nColor: {Color}\nHorsepower: {HorsePower}";
    }
}