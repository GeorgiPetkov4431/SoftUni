using System;

namespace _02._Vowels_Count;

internal class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();
        Console.WriteLine(GetVowelCount(input));
    }

    static int GetVowelCount(string input)
    {
        input = input.ToLower();
        int count = 0;
        foreach (char letter in input)
        {
            if (letter == 'a' || letter == 'e' || letter == 'i' || letter == 'o' || letter == 'u') count++;
        }
        return count;
    }
}
