using System;

namespace _04._Computer_Store;

internal class Program
{
    static void Main(string[] args)
    {
        decimal price = 0.0m;
        string command;

        while (true)
        {
            command = Console.ReadLine();
            if (command == "regular" || command == "special") break;
            decimal partPrice = decimal.Parse(command);
            if (partPrice < 0)
            {
                Console.WriteLine("Invalid price!");
                continue;
            }
            price += partPrice;
        }

        if (price == 0) Console.WriteLine("Invalid order!");
        else
        {
            decimal taxes = price * 0.2m;
            Console.WriteLine("Congratulations you've just bought a new computer!");
            Console.WriteLine($"Price without taxes: {price:f2}$");
            Console.WriteLine($"Taxes: {taxes:f2}$");
            Console.WriteLine("-----------");
            decimal priceWithTaxes = price + taxes;
            if (command == "special") priceWithTaxes *= 0.9m;
            Console.WriteLine($"Total price: {priceWithTaxes:f2}$");
        }
    }
}
