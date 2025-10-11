using System;
using System.Collections.Generic;

namespace _03._House_Party;

internal class Program
{
    static void Main(string[] args)
    {
        int commandNum = int.Parse(Console.ReadLine());
        List<string> guestList = new();

        for (int i = 0; i < commandNum; i++) {
            string[] command = Console.ReadLine().Split();

            string name = command[0];

            if (command[2] == "going!") AddGuest(guestList, name);
            else if (command[2] == "not") RemoveGuest(guestList, name);
        }

        Console.WriteLine(string.Join('\n', guestList));
    }

    static void AddGuest(List<string> guestList, string guest)
    {
        int guestIndex = guestList.FindIndex(e => e == guest);
        if (guestIndex == -1) guestList.Add(guest);
        else Console.WriteLine($"{guest} is already in the list!");
    }

    static void RemoveGuest(List<string> guestList, string guest)
    {
        if (guestList.Contains(guest)) guestList.Remove(guest);
        else Console.WriteLine($"{guest} is not in the list!");
    }
}
