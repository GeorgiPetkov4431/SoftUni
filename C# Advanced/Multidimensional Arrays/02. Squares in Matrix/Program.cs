using System;
using System.Linq;

namespace _02._Squares_in_Matrix;

internal class Program
{
    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine().Split().Select(int.Parse).ToArray();
        string[,] matrix = CreateMatrix(dimensions[0], dimensions[1], " ");

        int counter = 0;

        for (int i = 0; i < dimensions[0] - 1; i++)
        {
            for (int j = 0; j < dimensions[1] - 1; j++)
            {
                if (matrix[i, j] == matrix[i + 1, j] && matrix[i, j] == matrix[i, j + 1] && matrix[i, j] == matrix[i + 1, j + 1])
                {
                    counter++;
                }
            }
        }

        Console.WriteLine(counter);
    }

    static string[,] CreateMatrix(int rows, int cols, string separator)
    {
        string[,] matrix = new string[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            string[] currentRow = Console.ReadLine().Split(separator);

            for (int col = 0; col < cols; col++)
            {
                matrix[row, col] = currentRow[col];
            }
        }

        return matrix;
    }
}
