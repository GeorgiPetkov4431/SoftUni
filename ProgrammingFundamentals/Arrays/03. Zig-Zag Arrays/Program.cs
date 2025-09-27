using System;
using System.Linq;

namespace _03._Zig_Zag_Arrays;

internal class Program
{
    static void Main(string[] args)
    {
        int size = int.Parse(Console.ReadLine());

        int[] array1 = new int[size];
        int[] array2 = new int[size];

        for (int i = 0; i < size; i++) {
            int[] digits = Console.ReadLine().Split().Select(int.Parse).ToArray();
            if (i % 2 == 0) {
                array1[i] = digits[0];
                array2[i] = digits[1];
            } else {
                array1[i] = digits[1];
                array2[i] = digits[0];
            }
        }

        foreach (int i in array1) Console.Write($"{i} ");
        Console.WriteLine();
        foreach (int i in array2) Console.Write($"{i} ");
    }
}
