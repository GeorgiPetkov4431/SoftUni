using System;

namespace _07._NxN_Matrix;

internal class Program
{
    static void Main(string[] args)
    {
        int number = int.Parse(Console.ReadLine());
        Console.WriteLine(CreateMatrix(number));
    }

    static string CreateMatrix(int number)
    {
        string result = string.Empty;
        for (int i = 0; i < number; i++)
        {
            for (int j = 0; j < number; j++) result += $"{number} ";
            result += "\n";
        }
        return result;
    }
}
