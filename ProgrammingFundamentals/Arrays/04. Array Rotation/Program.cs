using System;
using System.Linq;

namespace _04._Array_Rotation;

internal class Program
{
    static void Main(string[] args)
    {
        int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int rotations = int.Parse(Console.ReadLine());

        /*for (int i = 0; i < rotations; i++) {
            int spare = array[0];

            for (int j = 0; j < array.Length - 1; j++) {
                array[j] = array[j + 1];
            }

            array[array.Length - 1] = spare;
        }*/

        //int lastRotations = rotations % array.Length;
        int[] rotatedArray = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            //rotatedArray[i] = array[(i + lastRotations) % array.Length];
            rotatedArray[i] = array[(i + rotations % array.Length) % array.Length];
        }

        Console.WriteLine(string.Join(" ", rotatedArray));
    }
}
