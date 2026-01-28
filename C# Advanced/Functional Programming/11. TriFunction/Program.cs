using System;

namespace _11._TriFunction;

internal class Program
{
    static void Main(string[] args)
    {
        int numSum = int.Parse(Console.ReadLine());
        string[] names = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Func<string, int, bool> matchesLength = (name, sum) =>
        {
            int charSum = 0;

            foreach (char c in name)
            {
                charSum += c;
            }

            return charSum >= sum;
        };

        Func<string[], Func<string, int, bool>, string> checkNames = (namesArr, function) =>
        {
            foreach (string name in namesArr)
            {
                if (function(name, numSum)) return name;
            }

            return null;
        };

        Console.WriteLine(checkNames(names, matchesLength));
    }


}
