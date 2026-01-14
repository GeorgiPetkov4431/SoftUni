using System;

namespace _01._Diagonal_Difference

{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            int[,] matrix = CreateMatrix(size, size, " ");

            int firstDiagonal = 0;
            int secondDiagonal = 0;

            for (int i = 0; i < size; i++)
            {
                firstDiagonal += matrix[i, i];
                secondDiagonal += matrix[size - i - 1, i];
            }

            Console.WriteLine(Math.Abs(firstDiagonal - secondDiagonal));
        }

        static int[,] CreateMatrix(int rows, int cols, string separator)
        {
            int[,] matrix = new int[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                string[] currentRow = Console.ReadLine().Split(separator);

                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = int.Parse(currentRow[col]);
                }
            }

            return matrix;
        }

        static void PrintMatrix(int[,] matrix)
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
    }
}
