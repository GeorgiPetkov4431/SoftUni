using System;
using System.Collections.Generic;

namespace _07._Append_Arrays;

internal class Program
{
    static void Main(string[] args)
    {
        string[] stringArrays = Console.ReadLine().Split('|', StringSplitOptions.RemoveEmptyEntries);

        List<string> symbols = ExtractSymbols(stringArrays);

        Console.WriteLine(string.Join(' ', symbols));
    }

    static List<string> ExtractSymbols(string[] stringArray)
    {
        List<string> result = new();

        for (int i = stringArray.Length - 1; i >= 0; i--)
        {
            string[] array = stringArray[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
            result.AddRange(array);
        }

        return result;
    }
}
