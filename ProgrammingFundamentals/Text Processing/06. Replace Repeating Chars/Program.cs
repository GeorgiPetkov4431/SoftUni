using System;
using System.Text;

namespace _06._Replace_Repeating_Chars;

internal class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();

        string result = ReplaceRepeatingCharacters(input);
        Console.WriteLine(result);
    }

    static string ReplaceRepeatingCharacters(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        StringBuilder result = new StringBuilder(input[0].ToString());

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] != input[i - 1]) result.Append(input[i]);
        }

        return result.ToString();
    }
}
