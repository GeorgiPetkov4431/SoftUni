using System;

namespace _01._Train;

internal class Program
{
    static void Main(string[] args)
    {
        int size = int.Parse(Console.ReadLine());
        int[] train = new int[size];
        int sum = 0;

        for (int i = 0; i < size; i++) {
            train[i] = int.Parse(Console.ReadLine());
            sum += train[i];
        }

        foreach (int cart in train) {
            Console.Write($"{cart} ");
        }

        Console.WriteLine($"\n{sum}");
    }
}
