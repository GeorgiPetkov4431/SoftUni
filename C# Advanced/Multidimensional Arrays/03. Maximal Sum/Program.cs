using System;
using System.Linq;

namespace _03._Maximal_Sum;

internal class Program
{
    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int[,] matrix = CreateMatrix(dimensions[0], dimensions[1], " ");

        if (dimensions[0] < 3 || dimensions[1] < 3)
        {
            return;
        }

        int maxSum = int.MinValue;
        int maxRow = 0;
        int maxCol = 0;

        for (int row = 0; row < dimensions[0] - 2; row++)
        {
            for (int col = 0; col < dimensions[1] - 2; col++)
            {
                int sum = SumSquare(matrix, row, col);
                if (sum > maxSum)
                {
                    maxSum = sum;

                    maxRow = row;
                    maxCol = col;
                }
            }
        }

        Console.WriteLine($"Sum = {maxSum}");
        Console.WriteLine($"{matrix[maxRow, maxCol]} {matrix[maxRow, maxCol + 1]} {matrix[maxRow, maxCol + 2]}");
        Console.WriteLine($"{matrix[maxRow + 1, maxCol]} {matrix[maxRow + 1, maxCol + 1]} {matrix[maxRow + 1, maxCol + 2]}");
        Console.WriteLine($"{matrix[maxRow + 2, maxCol]} {matrix[maxRow + 2, maxCol + 1]} {matrix[maxRow + 2, maxCol + 2]}");
    }

    static int SumSquare(int[,] matrix, int row, int col)
    {
        int sum = matrix[row, col] + matrix[row + 1, col] + matrix[row + 2, col]
                 + matrix[row, col + 1] + matrix[row + 1, col + 1] + matrix[row + 2, col + 1]
                 + matrix[row, col + 2] + matrix[row + 1, col + 2] + matrix[row + 2, col + 2];

        return sum;
    }

    static int[,] CreateMatrix(int rows, int cols, string separator)
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

    static int[] ReadIntArrayFromConsole(string separator)
    {
        return Console.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    }

    static string[] ReadStringArrayFromConsole(string separator)
    {
        return Console.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries);
    }
}
