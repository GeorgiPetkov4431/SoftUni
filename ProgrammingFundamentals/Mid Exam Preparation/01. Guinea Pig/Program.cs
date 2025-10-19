using System;

namespace _01._Guinea_Pig;

internal class Program
{
    static void Main(string[] args)
    {
        double monthlyFoodQuantity = double.Parse(Console.ReadLine()) * 1000;
        double monthlyHayQuantity = double.Parse(Console.ReadLine()) * 1000;
        double monthlyCoverQuantity = double.Parse(Console.ReadLine()) * 1000;

        double pigWeight = double.Parse(Console.ReadLine()) * 1000;

        bool isEnough = true;

        for (int i = 1; i <= 30; i++)
        {
            monthlyFoodQuantity -= 300;
            if (i % 2 == 0)
            {
                monthlyHayQuantity -= monthlyFoodQuantity * 0.05;
            }
            if (i % 3 == 0)
            {
                monthlyCoverQuantity -= pigWeight / 3.0;
            }
            if (monthlyFoodQuantity < 0 || monthlyHayQuantity < 0 || monthlyCoverQuantity < 0)
            {
                isEnough = false;
                break;
            }
        }

        if (isEnough) Console.WriteLine($"Everything is fine! Puppy is happy! Food: {monthlyFoodQuantity / 1000:F2}, Hay: {monthlyHayQuantity / 1000:F2}, Cover: {monthlyCoverQuantity / 1000:F2}.");
        else Console.WriteLine("Merry must go to the pet store!");
    }
}
