using System;

namespace _11._Orders;

internal class Program
{
    static void Main(string[] args)
    {
        int orderCount = int.Parse(Console.ReadLine());

        double totalCost = 0.0;

        for (int i = 0; i < orderCount; i++) {
            double pricePerCapsule = double.Parse(Console.ReadLine());
            int days = int.Parse(Console.ReadLine());
            int capsuleCount = int.Parse(Console.ReadLine());

            double price = capsuleCount * days * pricePerCapsule;
            price = Math.Round(price, 2);
            Console.WriteLine($"The price for the coffee is: ${price:F2}");
            totalCost += price;
        }

        Console.WriteLine($"Total: ${totalCost:F2}");
    }
}
