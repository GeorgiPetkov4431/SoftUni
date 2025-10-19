using System;

namespace _01._Cooking_Masterclass;

internal class Program
{
    static void Main(string[] args)
    {
        double budget = double.Parse(Console.ReadLine());
        int studentNum = int.Parse(Console.ReadLine());
        double flourPrice = double.Parse(Console.ReadLine());
        double eggsPrice = double.Parse(Console.ReadLine()) * 10;
        double apronsPrice = double.Parse(Console.ReadLine());

        //flourPrice = flourPrice * Math.Ceiling(studentNum * 1.2);
        flourPrice *= (studentNum - (studentNum / 5));
        eggsPrice *= studentNum;
        //apronsPrice += (studentNum * 0.8);
        apronsPrice *= Math.Ceiling(studentNum * 1.2);

        double totalPrice = flourPrice + eggsPrice + apronsPrice;

        if (totalPrice <= budget) Console.WriteLine($"Items purchased for {totalPrice:f2}$.");
        else Console.WriteLine($"{totalPrice - budget:f2}$ more needed.");
    }
}
