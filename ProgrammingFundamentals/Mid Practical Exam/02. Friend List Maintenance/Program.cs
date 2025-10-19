using System;
using System.Linq;
using System.Xml.Linq;

namespace _02._Friend_List_Maintenance;

internal class Program
{
    static void Main(string[] args)
    {
        string[] friendsList = Console.ReadLine().Split(", ");
        int blacklistedNames = 0;
        int lostNames = 0;

        string command;

        while ((command = Console.ReadLine()) != "Report")
        {
            string[] commandArgs = command.Split(" ");
            switch (commandArgs[0])
            {
                case "Blacklist":
                    blacklistedNames = Blacklist(friendsList, commandArgs[1], blacklistedNames);
                    break;
                case "Error":
                    lostNames = Error(friendsList, int.Parse(commandArgs[1]), lostNames);
                    break;
                case "Change":
                    Change(friendsList, int.Parse(commandArgs[1]), commandArgs[2]);
                    break;
            }
        }

        Console.WriteLine($"Blacklisted names: {blacklistedNames}");
        Console.WriteLine($"Lost names: {lostNames}");
        Console.WriteLine(string.Join(' ', friendsList));
    }

    static int Blacklist(string[] friendsList, string name, int count)
    {
        for (int i = 0; i < friendsList.Length; i++)
        {
            if (friendsList[i] == name)
            {
                friendsList[i] = "Blacklisted";
                Console.WriteLine($"{name} was blacklisted.");
                count++;
                return count;
            }
        }
        Console.WriteLine($"{name} was not found.");
        return count;
    }

    static int Error(string[] friendsList, int index, int count)
    {
        if (!IsValidIndex(friendsList.Length, index) || friendsList[index] == "Lost" || friendsList[index] == "Blacklisted") return count;
        Console.WriteLine($"{friendsList[index]} was lost due to an error.");
        friendsList[index] = "Lost";
        count++;
        return count;
    }

    static void Change(string[] friendsList, int index, string newName)
    {
        if (!IsValidIndex(friendsList.Length, index)) return;
        Console.WriteLine($"{friendsList[index]} changed his username to {newName}.");
        friendsList[index] = newName;
    }

    static bool IsValidIndex(int length, int index)
    {
        if (index < 0 || index >= length) return false;
        return true;
    }
}
