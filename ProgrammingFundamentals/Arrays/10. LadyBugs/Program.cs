using System;
using System.Linq;

namespace _10._LadyBugs;

internal class Program
{
    static void Main(string[] args)
    {
        int fieldSize = int.Parse(Console.ReadLine());
        int[] field = new int[fieldSize];
        int[] ladybugIndexes = Console.ReadLine().Split().Select(int.Parse).ToArray();
        
        for (int i = 0; i < ladybugIndexes.Length; i++) { 
            if (ladybugIndexes[i] >= 0 && ladybugIndexes[i] <= fieldSize - 1) {
                field[ladybugIndexes[i]] = 1;
            }
        }

        string command;
        while ((command = Console.ReadLine()) != "end") {
            string[] commandArr = command.Split();
            int index = int.Parse(commandArr[0]);
            string direction = commandArr[1];
            int distance = int.Parse(commandArr[2]);

            if (index >= 0 && index < fieldSize && field[index] == 1) {
                field[index] = 0;
                int movementIndex;
                switch (direction)
                {
                    case "left":
                        movementIndex = index - distance;
                        while (movementIndex >= 0 && movementIndex < fieldSize && field[movementIndex] == 1) {
                            movementIndex -= distance;
                        }

                        if (movementIndex >= 0 && movementIndex < fieldSize) {
                            field[movementIndex] = 1;
                        }

                        break;
                    case "right":
                        movementIndex = index + distance;
                        while (movementIndex >= 0 && movementIndex < fieldSize && field[movementIndex] == 1) {
                            movementIndex += distance;
                        }

                        if (movementIndex >= 0 && movementIndex < fieldSize) {
                            field[movementIndex] = 1;
                        }

                        break;
                }
            }
        }

        Console.WriteLine(string.Join(" ", field));
    }
}