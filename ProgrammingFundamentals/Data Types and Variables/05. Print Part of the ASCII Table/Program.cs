using System;

namespace _05._Print_Part_of_the_ASCII_Table;

internal class Program
{
    static void Main(string[] args)
    {
        short lowerBorder = short.Parse(Console.ReadLine());
        short upperBorder = short.Parse(Console.ReadLine());

        for (int i = lowerBorder; i <= upperBorder; i++) {
            Console.Write($"{(char)i} ");
        }
    }
}
