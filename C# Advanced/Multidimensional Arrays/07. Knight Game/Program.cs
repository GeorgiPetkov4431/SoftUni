using System;
using System.Linq;

namespace _07._Knight_Game;

internal class Program
{
    static void Main(string[] args)
    {
        int boardSize = int.Parse(Console.ReadLine());

        char[,] board = CreateCharMatrix(boardSize, boardSize);

        int removedKnights = 0;

        while (true)
        {
            int maxAttack = 0;
            int maxAttackRow = -1;
            int maxAttackCol = -1;

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (board[row, col] == 'K')
                    {
                        int attack = CalculateTotalAttacks(board, row, col);
                        if (attack > maxAttack)
                        {
                            maxAttack = attack;
                            maxAttackRow = row;
                            maxAttackCol = col;
                        }
                    }
                }
            }

            if (maxAttack == 0)
            {
                break;
            }
            else
            {
                board[maxAttackRow, maxAttackCol] = 'O';
                removedKnights++;
            }
        }

        Console.WriteLine(removedKnights);
    }

    static int CalculateTotalAttacks(char[,] board, int row, int col)
    {
        int totalAttacks = 0;

        if (IsValidCoordinate(board, row - 2, col - 1) && board[row - 2, col - 1] == 'K') totalAttacks++;
        if (IsValidCoordinate(board, row - 2, col + 1) && board[row - 2, col + 1] == 'K') totalAttacks++;
        if (IsValidCoordinate(board, row + 2, col - 1) && board[row + 2, col - 1] == 'K') totalAttacks++;
        if (IsValidCoordinate(board, row + 2, col + 1) && board[row + 2, col + 1] == 'K') totalAttacks++;
        if (IsValidCoordinate(board, row - 1, col - 2) && board[row - 1, col - 2] == 'K') totalAttacks++;
        if (IsValidCoordinate(board, row - 1, col + 2) && board[row - 1, col + 2] == 'K') totalAttacks++;
        if (IsValidCoordinate(board, row + 1, col - 2) && board[row + 1, col - 2] == 'K') totalAttacks++;
        if (IsValidCoordinate(board, row + 1, col + 2) && board[row + 1, col + 2] == 'K') totalAttacks++;

        return totalAttacks;
    }

    static bool IsValidCoordinate(char[,] board, int row, int col)
    {
        return row >= 0 && row < board.GetLength(0)
            && col >= 0 && col < board.GetLength(1);
    }

    static char[,] CreateCharMatrix(int rows, int cols)
    {
        char[,] matrix = new char[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            string currentRow = Console.ReadLine();

            for (int col = 0; col < cols; col++)
            {
                matrix[row, col] = currentRow[col];
            }
        }

        return matrix;
    }
}
