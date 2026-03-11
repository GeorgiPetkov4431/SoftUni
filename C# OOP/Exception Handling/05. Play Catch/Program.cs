using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayCatch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int totalExceptions = 0;

            while (totalExceptions != 3)
            {
                try
                {
                    string command = Console.ReadLine();

                    string[] commandArgs = command.Split(' ');

                    if (commandArgs[0] == "Replace")
                    {
                        int index = int.Parse(commandArgs[1]);
                        int element = int.Parse(commandArgs[2]);

                        numbers[index] = element;
                    }
                    else if (commandArgs[0] == "Print")
                    {
                        int startIndex = int.Parse(commandArgs[1]);
                        int endIndex = int.Parse(commandArgs[2]);

                        List<int> result = new List<int>();

                        for (int i = startIndex; i <= endIndex; i++)
                        {
                            result.Add(numbers[i]);
                        }

                        Console.WriteLine(string.Join(", ", result));
                    }
                    else if (commandArgs[0] == "Show")
                    {
                        int index = int.Parse(commandArgs[1]);

                        Console.WriteLine(numbers[index]);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("The variable is not in the correct format!");
                    totalExceptions++;
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The index does not exist!");
                    totalExceptions++;
                }
            }

            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}
