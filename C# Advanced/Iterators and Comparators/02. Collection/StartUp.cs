using System;
using System.Collections.Generic;
using System.Linq;

namespace Collection
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            ListyIterator<string> iterator = null;

            string command;

            while ((command = Console.ReadLine()) != "END")
            {
                List<string> commandArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

                if (commandArgs[0] == "Create")
                {
                    iterator = new ListyIterator<string>(commandArgs[1..]);
                }
                else if (commandArgs[0] == "Move")
                {
                    Console.WriteLine(iterator.MoveNext());
                }
                else if (commandArgs[0] == "Print")
                {
                    try
                    {
                        iterator.Print();
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (commandArgs[0] == "HasNext")
                {
                    Console.WriteLine(iterator.HasNext());
                }
                else if (commandArgs[0] == "PrintAll")
                {
                    foreach (var value in iterator)
                    {
                        Console.Write($"{value} ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
