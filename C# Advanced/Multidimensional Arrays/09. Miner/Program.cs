using System;
using System.Collections.Generic;
using System.Linq;

namespace _09._Miner;

internal class Program
{
    static void Main(string[] args)
    {
        int fieldSize = int.Parse(Console.ReadLine());
        Queue<string> directionCommands = new Queue<string>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries));

        char[,] field = new char[fieldSize, fieldSize];

        int totalCoals = 0;
        int posRow = -1;
        int posCol = -1;

        for (int row = 0; row < fieldSize; row++)
        {
            string[] currentRow = Console.ReadLine().Split();

            for (int col = 0; col < fieldSize; col++)
            {
                field[row, col] = currentRow[col][0];

                if (field[row, col] == 'c')
                {
                    totalCoals++;
                }
                if (field[row, col] == 's')
                {
                    posRow = row;
                    posCol = col;
                }
            }
        }

        bool isExit = false;

        while (directionCommands.Count > 0)
        {
            string command = directionCommands.Dequeue();

            switch (command)
            {
                case "up":
                    if (CoordinatesValid(field, posRow - 1, posCol))
                    {
                        posRow--;
                    }
                    break;
                case "down":
                    if (CoordinatesValid(field, posRow + 1, posCol))
                    {
                        posRow++;
                    }
                    break;
                case "left":
                    if (CoordinatesValid(field, posRow, posCol - 1))
                    {
                        posCol--;
                    }
                    break;
                case "right":
                    if (CoordinatesValid(field, posRow, posCol + 1))
                    {
                        posCol++;
                    }
                    break;
            }


            if (field[posRow, posCol] == 'c')
            {
                totalCoals--;
                field[posRow, posCol] = '*';
                if (totalCoals == 0)
                {
                    break;
                }
            }
            else if (field[posRow, posCol] == 'e')
            {
                Console.WriteLine($"Game over! ({posRow}, {posCol})");
                isExit = true;
                break;
            }
        }

        if (!isExit) {
            if (totalCoals > 0)
            {
                Console.WriteLine($"{totalCoals} coals left. ({posRow}, {posCol})");
            }
            else
            {
                Console.WriteLine($"You collected all coals! ({posRow}, {posCol})");
            }
        }
    }

    static bool CoordinatesValid(char[,] field, int row, int col)
    {
        return row >= 0 && row < field.GetLength(0) && col >= 0 && col < field.GetLength(1);
    }
}
