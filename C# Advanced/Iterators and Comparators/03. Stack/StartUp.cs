using System;
using System.Linq;

namespace Stack
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            CustomStack<string> stack = new CustomStack<string>();

            string command;

            while ((command = Console.ReadLine()) != "END")
            {
                if (command.StartsWith("Push"))
                {
                    string[] items = command.Substring(5).Split(", ", StringSplitOptions.RemoveEmptyEntries);
                    stack.Push(items);
                }
                else if (command == "Pop")
                {
                    try
                    {
                        stack.Pop();
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
        }
    }
}
