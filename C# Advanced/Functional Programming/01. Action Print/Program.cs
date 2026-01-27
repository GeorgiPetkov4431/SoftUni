using System;

namespace _01._Action_Print;

internal class Program
{
    static void Main(string[] args)
    {
        string[] namesArr = Console.ReadLine().Split(' ');
        Action<string[]> action = names => Console.WriteLine(string.Join("\n", names));
        action(namesArr);
    }
}
