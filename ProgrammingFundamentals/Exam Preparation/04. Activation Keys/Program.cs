using System;
using System.Text;

namespace _04._Activation_Keys;

internal class Program
{
    static void Main(string[] args)
    {
        string activationKey = Console.ReadLine();

        string command;
        while ((command = Console.ReadLine()) != "Generate")
        {
            string[] commandArgs = command.Split(">>>", StringSplitOptions.RemoveEmptyEntries);

            switch (commandArgs[0])
            {
                case "Contains":
                    string substring = commandArgs[1];
                    if (ContainsSubstring(activationKey, substring)) Console.WriteLine($"{activationKey} contains {substring}");
                    else Console.WriteLine("Substring not found!");
                    break;
                case "Flip":
                    string type = commandArgs[1];
                    int startIndex = int.Parse(commandArgs[2]);
                    int endIndex = int.Parse(commandArgs[3]);
                    activationKey = Flip(activationKey, type, startIndex, endIndex);
                    Console.WriteLine(activationKey);
                    break;
                case "Slice":
                    int startIndexSlice = int.Parse(commandArgs[1]);
                    int endIndexSlice = int.Parse(commandArgs[2]);
                    activationKey = Slice(activationKey, startIndexSlice, endIndexSlice);
                    Console.WriteLine(activationKey);
                    break;
            }
        }

        Console.WriteLine($"Your activation key is: {activationKey}");
    }

    static bool ContainsSubstring(string key, string substring)
    {
        return key.Contains(substring);
    }

    static string Flip(string key, string type, int startIndex, int endIndex)
    {
        string substring = key.Substring(startIndex, endIndex - startIndex);

        switch (type)
        {
            case "Upper":
                substring = substring.ToUpper();
                break;
            case "Lower":
                substring = substring.ToLower();
                break;
        }

        StringBuilder newKey = new StringBuilder();

        newKey.Append(key.Substring(0, startIndex));
        newKey.Append(substring);
        newKey.Append(key.Substring(endIndex, key.Length - endIndex));

        return newKey.ToString();
    }

    static string Slice(string key, int startIndex, int endIndex)
    {
        StringBuilder newKey = new StringBuilder();
        newKey.Append(key.Substring(0, startIndex));
        newKey.Append(key.Substring(endIndex, key.Length - endIndex));

        return newKey.ToString();
    }
}
