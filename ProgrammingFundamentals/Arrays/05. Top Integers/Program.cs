using System;
using System.Linq;

namespace _05._Top_Integers;

internal class Program
{
    static void Main(string[] args)
    {
        int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();

        for (int i = 0; i < array.Length; i++) {
            bool topInteger = true;
            for (int j = i + 1; j < array.Length; j++) {
                if (array[i] <= array[j]) {
                    topInteger = false;
                    break;
                }
            }
            if (topInteger) Console.Write($"{array[i]} ");
        }
    }
}