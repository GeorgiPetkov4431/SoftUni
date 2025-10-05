using System;

namespace _06._Middle_Characters;

internal class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();
        
        Console.WriteLine(PrintMiddleCharacters(input));
    }

    static string PrintMiddleCharacters(string sentence)
    {
        int middleIndex = sentence.Length / 2;

        string result = $"{sentence[middleIndex]}";

        if (sentence.Length % 2 == 0) result = $"{sentence[middleIndex - 1]}" + result;

        return result;
    }
}
