using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Change_List;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> intList = Console.ReadLine().Split().Select(int.Parse).ToList();

        string command;

        while ((command = Console.ReadLine()) != "end")
        {
            string[] commandArgs = command.Split();

            if (commandArgs[0] == "Delete") RemoveElement(intList, int.Parse(commandArgs[1]));
            else if (commandArgs[0] == "Insert") InsertElement(intList, int.Parse(commandArgs[1]), int.Parse(commandArgs[2]));
        }

        Console.WriteLine(string.Join(' ', intList));
    }

    static void RemoveElement(List<int> intList, int value)
    {
        for (int i = 0; i < intList.Count; i++) {
            if (intList[i] == value)
            {
                intList.RemoveAt(i);
                i--;
            }
        }
    }

    static void InsertElement(List<int> intList, int element, int position)
    {
        intList.Insert(position, element);
    }
}
