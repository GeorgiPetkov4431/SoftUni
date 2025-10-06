using System;

namespace _08._Factorial_Division;

internal class Program
{
    static void Main(string[] args)
    {
        long num1 = long.Parse(Console.ReadLine());
        long num2 = long.Parse(Console.ReadLine());

        //Console.WriteLine($"{DivideFactorials(num1, num2):F2}");
        Console.WriteLine($"{CalculateFactorial(num1) / CalculateFactorial(num2):F2}");
    }

    static double CalculateFactorial(long number)
    {
        double result = 1;

        for (long i = number; i > 0; i--) result *= i;

        return result;
    }
}
