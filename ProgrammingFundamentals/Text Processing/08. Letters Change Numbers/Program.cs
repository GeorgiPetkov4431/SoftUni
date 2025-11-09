using System;
using System.Text;

namespace _08._Letters_Change_Numbers;

internal class Program
{
    static void Main(string[] args)
    {
        string[] strings = Console.ReadLine().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

        double totalSum = 0;

        foreach (string val in strings)
        {
            char firstLetter = val[0];
            char lastLetter = val[val.Length - 1];
            ulong number = ulong.Parse(val.Substring(1, val.Length - 2));

            ulong position;
            double result = 0;

            if (char.IsUpper(firstLetter))
            {
                position = (ulong)(firstLetter - 'A' + 1);
                result = number / (double)position;
            }
            else if (char.IsLower(firstLetter))
            {
                position = (ulong)(firstLetter - 'a' + 1);
                result = number * (double)position;
            }

            if (char.IsUpper(lastLetter))
            {
                position = (ulong)(lastLetter - 'A' + 1);
                result -= position;
            }
            else if (char.IsLower(lastLetter))
            {
                position = (ulong)(lastLetter - 'a' + 1);
                result += position;
            }

            totalSum += result;
        }

        Console.WriteLine($"{totalSum:F2}");
    }
}
