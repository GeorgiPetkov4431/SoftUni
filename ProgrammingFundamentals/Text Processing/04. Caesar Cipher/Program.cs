using System;
using System.Text;

namespace _04._Caesar_Cipher;

internal class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();

        StringBuilder result = new StringBuilder();

        foreach (char c in input)
        {
            result.Append((char)(c + 3));
        }

        Console.WriteLine(result);
    }
}
