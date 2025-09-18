using System;

namespace _10._Rage_Expenses;

internal class Program
{
    static void Main(string[] args)
    {
        int lossCount = int.Parse(Console.ReadLine());
        double headsetCost = double.Parse(Console.ReadLine());
        double mouseCost = double.Parse(Console.ReadLine());
        double keyboardCost = double.Parse(Console.ReadLine());
        double displayCost = double.Parse(Console.ReadLine());

        double rageExpenses = headsetCost * (lossCount / 2) + mouseCost * (lossCount / 3) + keyboardCost * (lossCount / 6) + displayCost * (lossCount / 12);

        Console.WriteLine($"Rage expenses: {rageExpenses:F2} lv.");
    }
}
