using System;

namespace _05._Add_and_Subtract;

internal class Program
{
    static void Main(string[] args)
    {
        int num1 = int.Parse(Console.ReadLine());
        int num2 = int.Parse(Console.ReadLine());
        int num3 = int.Parse(Console.ReadLine());

        int result = AddNums(num1, num2);
        result = SubtractNums(result, num3);

        Console.WriteLine(result);
    }

    static int AddNums(int num1, int num2)
    {
        return num1 + num2;
    }

    static int SubtractNums(int num1, int num2)
    {
        return num1 - num2;
    }
}