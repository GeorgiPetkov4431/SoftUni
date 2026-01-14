using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Snake_Moves;

internal class Program
{
    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine().Split().Select(int.Parse).ToArray();
        char[,] matrix = new char[dimensions[0], dimensions[1]];

        string snake = Console.ReadLine();
        Queue<char> snakeQueue = new Queue<char>(snake);

        for (int row = 0; row < dimensions[0]; row++)
        {
            if (row % 2 == 0)
            {
                for (int col = 0; col < dimensions[1]; col++)
                {
                    matrix[row, col] = snakeQueue.Peek();
                    snakeQueue.Enqueue(snakeQueue.Dequeue());
                }
            }
            else
            {
                for (int col = dimensions[1] - 1; col >= 0; col--)
                {
                    matrix[row, col] = snakeQueue.Peek();
                    snakeQueue.Enqueue(snakeQueue.Dequeue());
                }
            }
        }

        PrintCharMatrix(matrix);
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

    static void PrintCharMatrix(char[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Console.Write($"{matrix[row, col]}");
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
