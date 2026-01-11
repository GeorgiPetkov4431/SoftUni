using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Songs_Queue;

internal class Program
{
    static void Main(string[] args)
    {
        Queue<string> playlist = new Queue<string>(Console.ReadLine().Split(", "));
        string command;

        while (playlist.Any())
        {
            command = Console.ReadLine();
            
            if (command == "Play")
            {
                playlist.Dequeue();
            }
            else if (command.StartsWith("Add"))
            {
                string song = command.Substring(4);
                //string song = command.Split()[1];
                if (!playlist.Contains(song))
                {
                    playlist.Enqueue(song);
                }
                else
                {
                    Console.WriteLine($"{song} is already contained!");
                }
            }
            else if (command == "Show")
            {
                Console.WriteLine(string.Join(", ", playlist));
            }
        }

        Console.WriteLine("No more songs!");
    }
}
