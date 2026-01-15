using System;
using System.Linq;
using System.Collections.Generic;

namespace _08._Bombs;

internal class Program
{
    static void Main(string[] args)
    {
        int fieldSize = int.Parse(Console.ReadLine());

        int[,] field = CreateIntMatrix(fieldSize, fieldSize, " ");

        string[] bombs = Console.ReadLine().Split();

        foreach (string bomb in bombs)
        {
            int[] bombCoordinates = bomb.Split(',').Select(int.Parse).ToArray();

            int bombRow = bombCoordinates[0];
            int bombCol = bombCoordinates[1];
            int bombPower = field[bombRow, bombCol];

            if (bombPower >= 0)
            {
                Explode(field, bombRow - 1, bombCol - 1, bombPower);
                Explode(field, bombRow - 1, bombCol, bombPower);
                Explode(field, bombRow - 1, bombCol + 1, bombPower);
                Explode(field, bombRow, bombCol - 1, bombPower);
                Explode(field, bombRow, bombCol, bombPower);
                Explode(field, bombRow, bombCol + 1, bombPower);
                Explode(field, bombRow + 1, bombCol - 1, bombPower);
                Explode(field, bombRow + 1, bombCol, bombPower);
                Explode(field, bombRow + 1, bombCol + 1, bombPower);
            }
        }

        int aliveCells = 0;
        int sumOfCells = 0;

        for (int row = 0; row < fieldSize; row++)
        {
            for (int col = 0; col < fieldSize; col++)
            {
                if (field[row, col] > 0)
                {
                    aliveCells++;
                    sumOfCells += field[row, col];
                }
            }
        }

        Console.WriteLine($"Alive cells: {aliveCells}");
        Console.WriteLine($"Sum: {sumOfCells}");

        PrintIntMatrix(field);
    }

    static void Explode(int[,] field, int row, int col, int power)
    {
        if (CoordinatesValid(field, row, col) && field[row, col] > 0)
        {
            field[row, col] -= power;
        }
    }

    static bool CoordinatesValid(int[,] field, int row, int col)
    {
        return row >= 0 && row < field.GetLength(0) && col >= 0 && col < field.GetLength(1);
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

    static int[] ReadIntArrayFromConsole(string separator)
    {
        return Console.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
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
}
