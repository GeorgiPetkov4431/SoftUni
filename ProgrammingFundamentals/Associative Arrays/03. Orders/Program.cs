using System;
using System.Linq;
using System.Collections.Generic;

namespace _03._Orders;

internal class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, Product> products = new();

        string command;

        while ((command = Console.ReadLine()) != "buy")
        {
            string[] commandArgs = command.Split();

            string name = commandArgs[0];
            decimal price = decimal.Parse(commandArgs[1]);
            decimal quantity = decimal.Parse(commandArgs[2]);

            if (!products.ContainsKey(name))
            {
                Product newProduct = new Product(name, price, quantity);
                products.Add(name, newProduct);
            }
            else
            {
                products[name].Update(price, quantity);
            }
        }

        foreach (KeyValuePair<string, Product> product in products)
        {
            Console.WriteLine(product.Value);
        }
    }
}

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }

    public Product(string name, decimal price, decimal quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public void Update(decimal price, decimal quantity)
    {
        Price = price;
        Quantity += quantity;
    }

    public decimal TotalPrice => Price * Quantity;

    public override string ToString()
    {
        return $"{Name} -> {TotalPrice:F2}";
    }
}
