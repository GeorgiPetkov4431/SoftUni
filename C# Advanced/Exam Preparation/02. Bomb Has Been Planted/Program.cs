using System;

namespace _02._Bomb_Has_Been_Planted
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] mapDimensions = Console.ReadLine().Split(new char[] {' ', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int n = mapDimensions[0];
            int m = mapDimensions[1];

            char[,] map = new char[n, m];

            int counterTerroristRow = -1;
            int counterTerroristCol = -1;

            int bombRow = -1;
            int bombCol = -1;

            for (int row = 0; row < n; row++)
            {
                string input = Console.ReadLine();

                for (int col = 0; col < m; col++)
                {
                    map[row, col] = input[col];

                    if (input[col] == 'C')
                    {
                        counterTerroristRow = row;
                        counterTerroristCol = col;
                    }
                    else if (input[col] == 'B')
                    {
                        bombRow = row;
                        bombCol = col;
                    }
                }
            }

            int counter = 16;

            string command;

            bool defused = false;
            bool dead = false;

            while (counter > 0)
            {
                counter--;
                command = Console.ReadLine();

                switch (command)
                {
                    case "up":
                        if (CheckIfValidMove(counterTerroristRow - 1, n)) counterTerroristRow--;
                        break;
                    case "down":
                        if (CheckIfValidMove(counterTerroristRow + 1, n)) counterTerroristRow++;
                        break;
                    case "left":
                        if (CheckIfValidMove(counterTerroristCol - 1, m)) counterTerroristCol--;
                        break;
                    case "right":
                        if (CheckIfValidMove(counterTerroristCol + 1, m)) counterTerroristCol++;
                        break;
                    case "defuse":
                        if (map[counterTerroristRow, counterTerroristCol] == 'B')
                        {
                            counter-=3;
                            if (counter >= 0)
                            {
                                map[counterTerroristRow, counterTerroristCol] = 'D';
                                defused = true;
                            }
                        }
                        else
                        {
                            counter--;
                            continue;
                        }
                        break;
                }

                if (defused)
                {
                    break;
                }

                if (map[counterTerroristRow, counterTerroristCol] == 'T')
                {
                    map[counterTerroristRow, counterTerroristCol] = '*';
                    dead = true;
                    break;
                }
            }

            if (defused)
            {
                Console.WriteLine("Counter-terrorist wins!");
                Console.WriteLine($"Bomb has been defused: {counter} second/s remaining.");
            }
            else
            {
                Console.WriteLine("Terrorists win!");

                if (!dead)
                {
                    if (counter < 0) map[bombRow, bombCol] = 'X';
                    Console.WriteLine("Bomb was not defused successfully!");
                    Console.WriteLine($"Time needed: {Math.Abs(counter)} second/s.");
                }
            }

            PrintCharMatrix(map);
        }

        static bool CheckIfValidMove(int coordinate, int mapLength)
        {
            return coordinate >= 0 && coordinate < mapLength;
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
}
