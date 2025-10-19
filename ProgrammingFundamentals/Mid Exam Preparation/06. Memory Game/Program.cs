using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace _06._Memory_Game;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> elements = Console.ReadLine().Split().ToList();
        string command;
        int moveNum = 0;

        while ((command = Console.ReadLine()) != "end")
        {
            if (elements.Count == 0) continue;
            moveNum++;
            int[] elementIndexes = command.Split().Select(int.Parse).ToArray();
            if (elementIndexes[0] == elementIndexes[1] || IsOutOfBounds(elements.Count, elementIndexes[0]) || IsOutOfBounds(elements.Count, elementIndexes[1]))
            {
                Console.WriteLine("Invalid input! Adding additional elements to the board");
                elements.Insert(elements.Count / 2, $"-{moveNum}a");
                elements.Insert(elements.Count / 2, $"-{moveNum}a");
            } else
            {
                if (elements[elementIndexes[0]] == elements[elementIndexes[1]])
                {
                    Console.WriteLine($"Congrats! You have found matching elements - {elements[elementIndexes[0]]}!");
                    if (elementIndexes[0] > elementIndexes[1])
                    {
                        elements.RemoveAt(elementIndexes[0]);
                        elements.RemoveAt(elementIndexes[1]);
                    }
                    else
                    {
                        elements.RemoveAt(elementIndexes[1]);
                        elements.RemoveAt(elementIndexes[0]);
                    }
                }
                else Console.WriteLine("Try again!");
            }
        }

        if (elements.Count == 0)
        {
            Console.WriteLine($"You have won in {moveNum} turns!");
        }
        else
        {
            Console.WriteLine("Sorry you lose :(");
            Console.WriteLine(string.Join(' ', elements));
        }
    }

    static bool IsOutOfBounds(int length, int index)
    {
        if (index < 0 || index >= length) return true;
        return false;
    }
}
