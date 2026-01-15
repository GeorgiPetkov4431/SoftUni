using System;

namespace _10._Radioactive_Mutant_Vampire_Bunnies;

internal class Program
{
    static void Main(string[] args)
    {
        int[] dimensions = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int rows = dimensions[0];
        int cols = dimensions[1];

        char[,] field = new char[rows, cols];

        int playerRow = -1;
        int playerCol = -1;

        for (int row = 0; row < rows; row++)
        {
            string input = Console.ReadLine();

            for (int col = 0 ; col < cols; col++)
            {
                field[row, col] = input[col];

                if (field[row, col] == 'P')
                {
                    playerRow = row;
                    playerCol = col;
                    field[row, col] = '.';
                }
            }
        }

        string commands = Console.ReadLine();

        bool isDead = false;

        foreach (char command in commands)
        {
            int lastPlayerRow = playerRow;
            int lastPlayerCol = playerCol;
            switch (command)
            {
                case 'L':
                    playerCol--;
                    break;
                case 'R':
                    playerCol++;
                    break;
                case 'U':
                    playerRow--;
                    break;
                case 'D':
                    playerRow++;
                    break;
            }

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (field[row, col] == 'B')
                    {
                        if (ValidCoordinates(field, row - 1, col) && field[row - 1, col] == '.')
                        {
                            field[row - 1, col] = 'b';
                        }
                        if (ValidCoordinates(field, row + 1, col) && field[row + 1, col] == '.')
                        {
                            field[row + 1, col] = 'b';
                        }
                        if (ValidCoordinates(field, row, col - 1) && field[row, col - 1] == '.')
                        {
                            field[row, col - 1] = 'b';
                        }
                        if (ValidCoordinates(field, row, col + 1) && field[row, col + 1] == '.')
                        {
                            field[row, col + 1] = 'b';
                        }
                    }
                }
            }

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (field[row, col] == 'b') field[row, col] = 'B';
                }
            }

            if (ValidCoordinates(field, playerRow, playerCol))
            {
                if (field[playerRow, playerCol] == 'B')
                {
                    isDead = true;
                    break;
                }
            }
            else
            {
                playerRow = lastPlayerRow;
                playerCol = lastPlayerCol;
                break;
            }
        }

        PrintCharMatrix(field);

        if (isDead)
        {
            Console.WriteLine($"dead: {playerRow} {playerCol}");
        }
        else
        {
            Console.WriteLine($"won: {playerRow} {playerCol}");
        }
    }

    static void MultiplyBunnies()
    {

    }

    static bool ValidCoordinates(char[,] field, int row, int col)
    {
        return row >= 0 && row < field.GetLength(0) && col >= 0 && col < field.GetLength(1);
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
}
