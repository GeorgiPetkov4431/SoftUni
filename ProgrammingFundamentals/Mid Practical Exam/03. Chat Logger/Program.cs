using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Chat_Logger;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> chat = new();

        string command;

        while ((command = Console.ReadLine()) != "end")
        {
            string[] commandArgs = command.Split();
            string message = commandArgs[1];
            switch (commandArgs[0])
            {
                case "Chat":
                    chat.Add(commandArgs[1]);
                    break;
                case "Delete":
                    chat.Remove(commandArgs[1]);
                    break;
                case "Edit":
                    if (chat.Contains(message)) chat[chat.IndexOf(message)] = commandArgs[2];
                    break;
                case "Pin":
                    if (chat.Contains(message))
                    {
                        chat.Add(message);
                        chat.Remove(message);
                    }
                    break;
                case "Spam":
                    List<string> messages = commandArgs.Skip(1).Take(commandArgs.Length - 1).ToList();
                    chat.AddRange(messages);
                    break;
            }
        }

        Console.WriteLine(string.Join("\n", chat));
    }
}
