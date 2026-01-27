using System;

namespace _02._Knights_of_Honor;

internal class Program
{
    static void Main(string[] args)
    {
        string[] namesArr = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Action<string[]> action = names =>
        {
            foreach (string name in names)
            {
                Console.WriteLine($"Sir {name}");
            }
        };
        action(namesArr);
    }
}
