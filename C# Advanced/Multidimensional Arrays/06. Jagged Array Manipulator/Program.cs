using System;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _06._Jagged_Array_Manipulator;

internal class Program
{
    static void Main(string[] args)
    {
        int rows = int.Parse(Console.ReadLine());
        int[][] jaggedArray = CreateJaggedIntArray(rows, " ");

        MultiplyOrDivideArray(jaggedArray);

        string command;

        while ((command = Console.ReadLine().ToLower()) != "end")
        {
            string[] commandArgs = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int row = int.Parse(commandArgs[1]);
            int column = int.Parse(commandArgs[2]);
            int value = int.Parse(commandArgs[3]);

            switch (commandArgs[0])
            {
                case "add":
                    if (IsValidIndex(row, jaggedArray.Length) && IsValidIndex(column, jaggedArray[row].Length))
                    {
                        jaggedArray[row][column] += value;
                    }
                    break;
                case "subtract":
                    if (IsValidIndex(row, jaggedArray.Length) && IsValidIndex(column, jaggedArray[row].Length))
                    {
                        jaggedArray[row][column] -= value;
                    }
                    break;
            }
            
        }

        foreach (int[] array in jaggedArray)
        {
            Console.WriteLine(string.Join(" ", array));
        }
    }

    static bool IsValidIndex(int index, int maxIndex)
    {
        if (index < 0 || index >= maxIndex) return false;
        return true;
    }

    static int[][] CreateJaggedIntArray(int rows, string separator)
    {
        int[][] jaggedArray = new int[rows][];

        for (int row = 0; row < rows; row++)
        {
            int[] arrInput = Console.ReadLine().Split(separator).Select(int.Parse).ToArray();
            jaggedArray[row] = arrInput;
        }

        return jaggedArray;
    }

    static void MultiplyOrDivideArray(int[][] jaggedArray)
    {
        for (int row = 0; row < jaggedArray.Length - 1; row++)
        {
            if (jaggedArray[row].Length == jaggedArray[row + 1].Length)
            {
                Multiply(jaggedArray[row]);
                Multiply(jaggedArray[row + 1]);
            }
            else
            {
                Divide(jaggedArray[row]);
                Divide(jaggedArray[row + 1]);
            }
        }
    }

    static void Multiply(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] *= 2;
        }
    }

    static void Divide(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] /= 2;
        }
    }
}
