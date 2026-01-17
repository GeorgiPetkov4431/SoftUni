using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Count_Symbols;

internal class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();

        SortedDictionary<char, int> letters = new SortedDictionary<char, int>();

        foreach (char c in input)
        {
            if (!letters.ContainsKey(c))
            {
                letters.Add(c, 0);
            }
            letters[c]++;
        }
        
        foreach (var letter in letters)
        {
            Console.WriteLine($"{letter.Key}: {letter.Value} time/s");
        }
    }
}
