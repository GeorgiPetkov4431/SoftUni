using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _01._Furniture;

internal class Program
{
    static void Main(string[] args)
    {
        List<Furniture> furnitures = new List<Furniture>();

        string pattern = @"(?<Name>[A-z]+)<<(?<Price>\d+\.\d+|\d+)!(?<Quantity>\d+)";

        Regex regex = new(pattern);

        string command;
        while ((command = Console.ReadLine()) != "Purchase")
        {
            Match match = regex.Match(command);
            if (match.Success)
            {
                string name = match.Groups["Name"].Value;
                decimal price = decimal.Parse(match.Groups["Price"].Value);
                int quantity = int.Parse(match.Groups["Quantity"].Value);

                Furniture newFurniture = new Furniture(name, price, quantity);
                furnitures.Add(newFurniture);
            }
        }

        Console.WriteLine("Bought furniture:");
        decimal totalSpend = 0m;
        foreach (Furniture f in  furnitures)
        {
            Console.WriteLine(f.Name);
            totalSpend += f.TotalPrice;
        }

        Console.WriteLine($"Total money spend: {totalSpend:F2}");
    }
}

class Furniture
{
    public string Name { get; set; }
    private decimal Price { get; set; }
    private int Quantity { get; set; }

    public decimal TotalPrice
    {
        get { return Price * Quantity; }
    }

    public Furniture(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}