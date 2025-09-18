using System;

namespace _09._Padawan_Equipment;

internal class Program
{
    static void Main(string[] args)
    {
        double balance = double.Parse(Console.ReadLine());
        int students = int.Parse(Console.ReadLine());
        double lightSaberCost = double.Parse(Console.ReadLine());
        double robeCost = double.Parse(Console.ReadLine());
        double beltCost = double.Parse(Console.ReadLine());

        double totalCost = 0.0;

        totalCost = lightSaberCost * Math.Ceiling(students * 1.1) + robeCost * students + beltCost * (students - students / 6);

        if (balance >= totalCost) Console.WriteLine($"The money is enough - it would cost {totalCost:F2}lv.");
        else Console.WriteLine($"John will need {(totalCost - balance):F2}lv more.");
    }
}
