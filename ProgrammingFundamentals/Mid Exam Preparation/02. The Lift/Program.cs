using System;
using System.Linq;

namespace _02._The_Lift;

internal class Program
{
    static void Main(string[] args)
    {
        int people = int.Parse(Console.ReadLine());
        int[] lift = Console.ReadLine().Split().Select(int.Parse).ToArray();

        for (int i = 0; i < lift.Length; i++)
        {
            for (int j = lift[i]; j < 4; j++)
            {
                if (people > 0)
                {
                    people--;
                    lift[i]++;
                }
            }
        }

        if (people == 0 && lift.Sum() < lift.Length * 4) Console.WriteLine("The lift has empty spots!");
        else if (people > 0 && lift.Sum() == lift.Length * 4) Console.WriteLine($"There isn't enough space! {people} people in a queue!");
        Console.WriteLine(string.Join(' ', lift));
    }
}
