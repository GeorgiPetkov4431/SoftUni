using System;
using System.Collections.Generic;
using System.Linq;

namespace _09._Simple_Text_Editor;

internal class Program
{
    static void Main(string[] args)
    {
        int numOps = int.Parse(Console.ReadLine());

        string text = string.Empty;
        Stack<string> textHistory = new Stack<string>();

        string command;

        for (int i = 0; i < numOps; i++)
        {
            command = Console.ReadLine();
            if (command.StartsWith("1"))
            {
                textHistory.Push(text);
                text += command.Substring(2);
            }
            else if (command.StartsWith("2"))
            {
                textHistory.Push(text);
                int length = int.Parse(command.Substring(2));
                if (length > text.Length)
                {
                    text = string.Empty;
                }
                else
                {
                    text = text.Substring(0, text.Length - length);
                }
            }
            else if (command.StartsWith("3"))
            {
                int index = int.Parse(command.Substring(2));
                Console.WriteLine(text[index - 1]);
            }
            else if (command.StartsWith("4"))
            {
                text = textHistory.Pop();
            }
        }
    }
}
