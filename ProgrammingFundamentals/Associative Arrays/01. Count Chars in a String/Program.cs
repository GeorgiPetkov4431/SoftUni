using System;
using System.Collections.Generic;

namespace _01._Count_Chars_in_a_String;

internal class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();

        Dictionary<char, int> characterOccurrences = new();

        foreach (char c in input)
        {
            if (c == ' ') continue;

            if (!characterOccurrences.ContainsKey(c))
            {
                characterOccurrences.Add(c, 0);
            }

            characterOccurrences[c]++;
        }

        foreach (KeyValuePair<char, int> pair in characterOccurrences)
        {
            Console.WriteLine($"{pair.Key} -> {pair.Value}");
        }
    }
}
