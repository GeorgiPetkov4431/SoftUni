using System;
using System.Linq;

namespace _07._Max_Sequence_of_Equal_Elements;

internal class Program
{
    static void Main(string[] args)
    {
        int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int maxLen = 1;
        int maxStart = 0;

        int currentLen = 1;
        int currentStart = 0;

        for (int i = 0; i < array.Length - 1; i++) {
            if (array[i] == array[i + 1]) currentLen++;
            else {
                currentLen = 1;
                currentStart = i + 1;
            }

            if (currentLen > maxLen) {
                maxLen = currentLen;
                maxStart = currentStart;
            }
        }

        for (int i = 0; i < maxLen; i++) {
            Console.Write($"{array[i + maxStart]} ");
        }
    }
}
