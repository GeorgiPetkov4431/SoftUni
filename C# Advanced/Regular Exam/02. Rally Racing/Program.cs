using System;

namespace _02._Rally_Racing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            string racingNumber = Console.ReadLine();

            char[,] track = new char[n, n];

            int carRow = 0;
            int carCol = 0;

            int tunnelOneRow = -1;
            int tunnelOneCol = -1;
            bool tunnelOneCreated = false;
            int tunnelTwoRow = -1;
            int tunnelTwoCol = -1;

            for (int row = 0; row < n; row++)
            {
                string[] input = Console.ReadLine().Split(' ');

                for (int col = 0; col < n; col++)
                {
                    track[row, col] = input[col][0];
                    if (input[col][0] == 'T')
                    {
                        if (!tunnelOneCreated)
                        {
                            tunnelOneRow = row;
                            tunnelOneCol = col;
                            tunnelOneCreated = true;
                            continue;
                        }

                        tunnelTwoRow = row;
                        tunnelTwoCol = col;
                    }
                }
            }

            int kilometersPassed = 0;
            bool raceCompleted = false;

            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                kilometersPassed += 10;

                switch (command)
                {
                    case "up":
                        if (CheckIfValidCoordinate(carRow - 1, n)) carRow--;
                        break;
                    case "down":
                        if (CheckIfValidCoordinate(carRow + 1, n)) carRow++;
                        break;
                    case "left":
                        if (CheckIfValidCoordinate(carCol - 1, n)) carCol--;
                        break;
                    case "right":
                        if (CheckIfValidCoordinate(carCol + 1, n)) carCol++;
                        break;
                }

                if (track[carRow, carCol] == 'F')
                {
                    raceCompleted = true;
                    break;
                }

                if (track[carRow, carCol] == 'T')
                {
                    if (carRow == tunnelOneRow && carCol == tunnelOneCol)
                    {
                        carRow = tunnelTwoRow;
                        carCol = tunnelTwoCol;
                    }
                    else
                    {
                        carRow = tunnelOneRow;
                        carCol = tunnelOneCol;
                    }

                    track[tunnelOneRow, tunnelOneCol] = '.';
                    track[tunnelTwoRow, tunnelTwoCol] = '.';
                    kilometersPassed += 20;
                }
            }

            track[carRow, carCol] = 'C';

            if (raceCompleted)
            {
                Console.WriteLine($"Racing car {racingNumber} finished the stage!");
            }
            else
            {
                Console.WriteLine($"Racing car {racingNumber} DNF.");
            }

            Console.WriteLine($"Distance covered {kilometersPassed} km.");
            PrintCharMatrix(track);
        }

        static bool CheckIfValidCoordinate(int coordinate, int trackSize)
        {
            return coordinate >= 0 && coordinate < trackSize;
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
