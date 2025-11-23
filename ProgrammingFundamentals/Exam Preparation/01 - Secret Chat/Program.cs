using System;

namespace _01___Secret_Chat;

internal class Program
{
    static void Main(string[] args)
    {
        string message = Console.ReadLine();

        string command;

        while ((command = Console.ReadLine()) != "Reveal")
        {
            string[] commandArgs = command.Split(":|:", StringSplitOptions.RemoveEmptyEntries);

            switch (commandArgs[0])
            {
                case "InsertSpace":
                    int index = int.Parse(commandArgs[1]);
                    message = InsertSpace(message, index);
                    break;
                case "Reverse":
                    string reverseSubstring = commandArgs[1];
                    message = ReverseSubstring(message, reverseSubstring);
                    break;
                case "ChangeAll":
                    string changeSubstring = commandArgs[1];
                    string replacement = commandArgs[2];
                    message = ChangeAll(message, changeSubstring, replacement);
                    break;
            }
        }

        Console.WriteLine($"You have a new text message: {message}");
    }

    static string InsertSpace(string message, int index)
    {
        if (!CheckIfValidIndex(message, index)) return message;
        message = message.Insert(index, " ");
        Console.WriteLine(message);
        return message;
    }

    static string ReverseSubstring(string message, string substring)
    {
        int index = message.IndexOf(substring);
        if (index == -1)
        {
            Console.WriteLine("error");
            return message;
        }
        message = message.Remove(index, substring.Length);

        for (int i = substring.Length - 1; i >= 0; i--)
        {
            message += substring[i];
        }

        Console.WriteLine(message);
        return message;
    }

    static string ChangeAll(string message, string substring, string replacement)
    {
        message = message.Replace(substring, replacement);
        Console.WriteLine(message);
        return message;
    }

    private static bool CheckIfValidIndex(string input, int index)
    {
        return (index >= 0 && index < input.Length - 1);
    }
}