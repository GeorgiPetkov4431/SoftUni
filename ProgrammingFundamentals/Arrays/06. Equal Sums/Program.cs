using System;
using System.Linq;

namespace _06._Equal_Sums;

internal class Program
{
    static void Main(string[] args)
    {
        int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
        bool exists = false;

        for (int i = 0; i < array.Length; i++) {
            int sum1 = 0;
            int sum2 = 0;

            for (int j = 0; j < i; j++) {
                sum1 += array[j];
            }
            for (int j = i + 1; j < array.Length; j++) {
                sum2 += array[j];
            }

            if (sum1 == sum2) {
                Console.WriteLine(i);
                exists = true;
                break;
            }
        }

        if (!exists) Console.WriteLine("no");
    }
}
