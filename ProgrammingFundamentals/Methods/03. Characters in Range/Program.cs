using System;

namespace _03._Characters_in_Range;

internal class Program
{
    static void Main(string[] args)
    {
        char firstChar = char.Parse(Console.ReadLine());
        char secondChar = char.Parse(Console.ReadLine());

        if (firstChar > secondChar)
        {
            char spare = secondChar;
            secondChar = firstChar;
            firstChar = spare;
        }

        PrintCharsInRange(firstChar, secondChar);
    }

    static void PrintCharsInRange(char char1, char char2)
    {
        for (int i = char1 + 1; i < char2; i++)
        {
            Console.Write($"{(char)i} ");
        }
    }
}
