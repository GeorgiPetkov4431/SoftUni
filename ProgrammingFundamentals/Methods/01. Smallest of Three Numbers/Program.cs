using System;

namespace _01._Smallest_of_Three_Numbers;

internal class Program
{
    static void Main(string[] args)
    {
        int num1 = int.Parse(Console.ReadLine());
        int num2 = int.Parse(Console.ReadLine());
        int num3 = int.Parse(Console.ReadLine());

        int smallestNum = GetSmallestNum(num1, num2);
        smallestNum = GetSmallestNum(smallestNum, num3);

        Console.WriteLine(smallestNum);
    }

    public static int GetSmallestNum(int num1, int num2)
    {
        if (num1 < num2) return num1;
        return num2;
    }
}