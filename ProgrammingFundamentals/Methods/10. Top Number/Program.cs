using System;

namespace _10._Top_Number;

internal class Program
{
    static void Main(string[] args)
    {
        int number = int.Parse(Console.ReadLine());

        for (int i = 1; i <= number; i++) {
            if (IsDivisibleByEight(i) && HoldsOddDigit(i)) Console.WriteLine(i);
        }
    }

    static bool IsDivisibleByEight(int number)
    {
        int sum = 0;
        while (number > 0) {
            sum += number % 10;
            number /= 10;
        }
        return sum % 8 == 0;
    }

    static bool HoldsOddDigit(int number)
    {
        while (number > 0)
        {
            if (number % 2 == 1) return true;
            number /= 10;
        }
        return false;
    }
}
