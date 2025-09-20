using System;

namespace _02._Sum_Digits;

internal class Program
{
    static void Main(string[] args)
    {
        int number = int.Parse(Console.ReadLine());
        int length = (int)Math.Log10(number) + 1;

        int sum = 0;

        for (int i = 0; i < length; i++) {
            sum += (number % 10);
            number /= 10;
        }

        Console.WriteLine(sum);
    }
}
