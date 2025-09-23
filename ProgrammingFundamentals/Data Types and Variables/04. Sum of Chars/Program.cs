using System;

namespace _04._Sum_of_Chars;

internal class Program
{
    static void Main(string[] args)
    {
        byte charCount = byte.Parse(Console.ReadLine());

        int charSum = 0;

        for (int i = 0; i < charCount; i++) {
            char input = char.Parse(Console.ReadLine());
            charSum += input;
        }
        Console.WriteLine($"The sum equals: {charSum}");
    }
}
