using System;
using System.Linq;

namespace _04._Matrix_Shuffling;

internal class Program
{
    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine().Split().Select(int.Parse).ToArray();
        string[,] matrix = CreateStringMatrix(dimensions[0], dimensions[1], " ");

        string command;

        while ((command = Console.ReadLine().ToLower()) != "end")
        {
            string[] commandArgs = command.Split();
            if (commandArgs[0] == "swap" && commandArgs.Length == 5)
            {
                int row = int.Parse(commandArgs[1]);
                int col = int.Parse(commandArgs[2]);
                int rowSwap = int.Parse(commandArgs[3]);
                int colSwap = int.Parse(commandArgs[4]);

                if (IsValidIndex(row, col, dimensions[0], dimensions[1]) && IsValidIndex(rowSwap, colSwap, dimensions[0], dimensions[1]))
                {
                    string temp = matrix[row, col];
                    matrix[row, col] = matrix[rowSwap, colSwap];
                    matrix[rowSwap, colSwap] = temp;

                    PrintStringMatrix(matrix);
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
        }
    }

    static void PrintIntMatrix(int[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Console.Write($"{matrix[row, col]} ");
            }
            Console.WriteLine();
        }
    }

    static void PrintStringMatrix(string[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Console.Write($"{matrix[row, col]} ");
            }
            Console.WriteLine();
        }
    }

    static int[,] CreateIntMatrix(int rows, int cols, string separator)
    {
        int[,] matrix = new int[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            int[] currentRow = ReadIntArrayFromConsole(separator);

            for (int col = 0; col < cols; col++)
            {
                matrix[row, col] = currentRow[col];
            }
        }

        return matrix;
    }

    static string[,] CreateStringMatrix(int rows, int cols, string separator)
    {
        string[,] matrix = new string[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            string[] currentRow = ReadStringArrayFromConsole(separator);

            for (int col = 0; col < cols; col++)
            {
                matrix[row, col] = currentRow[col];
            }
        }

        return matrix;
    }

    static int[] ReadIntArrayFromConsole(string separator)
    {
        return Console.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    }

    static string[] ReadStringArrayFromConsole(string separator)
    {
        return Console.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries);
    }

    static bool IsValidIndex(int row, int col, int rows, int cols)
    {
        if (row < 0 || col < 0 || row >= rows || col >= cols) return false;
        return true;
    }
}
