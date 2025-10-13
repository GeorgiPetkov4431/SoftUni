using System;
using System.Linq;

namespace _10._LadyBugs;

internal class Program
{
    static void Main(string[] args)
    {
        int fieldSize = int.Parse(Console.ReadLine());
        int[] field = new int[fieldSize];
        int[] startingIndexes = Console.ReadLine().Split().Select(int.Parse).ToArray();

        for (int i = 0; i < startingIndexes.Length; i++)
        {
            if (startingIndexes[i] >= 0 && startingIndexes[i] < fieldSize) field[startingIndexes[i]] = 1;
        }

        string command;

        while ((command = Console.ReadLine()) != "end")
        {
            string[] commandArgs = command.Split();
            int currentIndex = int.Parse(commandArgs[0]);
            string direction = commandArgs[1];
            int distance = int.Parse(commandArgs[2]);

            if (currentIndex >= 0 && currentIndex < fieldSize && field[currentIndex] == 1)
            {
                field[currentIndex] = 0;
                int movementIndex;

                switch (direction)
                {
                    case "left":
                        movementIndex = currentIndex - distance;
                        while (movementIndex >= 0 && movementIndex < fieldSize && field[movementIndex] == 1) movementIndex -= distance;

                        if (movementIndex >= 0 && movementIndex < fieldSize) field[movementIndex] = 1;

                        break;
                    case "right":
                        movementIndex = currentIndex + distance;
                        while (movementIndex >= 0 && movementIndex < fieldSize && field[movementIndex] == 1) movementIndex += distance;

                        if (movementIndex >= 0 && movementIndex < fieldSize) field[movementIndex] = 1;

                        break;
                }
            }
        }

        Console.WriteLine(string.Join(' ', field));
    }
}