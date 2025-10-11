using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._List_Operations;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> list = Console.ReadLine().Split().Select(int.Parse).ToList();

        string command;
        while ((command = Console.ReadLine()) != "End")
        {
            string[] commandArgs = command.Split();

            switch (commandArgs[0])
            {
                case "Add":
                    list.Add(int.Parse(commandArgs[1]));
                    break;
                case "Insert":
                    int number = int.Parse(commandArgs[1]);
                    int insertIndex = int.Parse(commandArgs[2]);
                    if (IsValidIndex(list.Count, insertIndex)) list.Insert(insertIndex, number);
                    else Console.WriteLine("Invalid index");
                    break;
                case "Remove":
                    int removeIndex = int.Parse(commandArgs[1]);
                    if (IsValidIndex(list.Count, removeIndex)) list.RemoveAt(removeIndex);
                    else Console.WriteLine("Invalid index");
                    break;
                case "Shift":
                    string direction = commandArgs[1];
                    int count = int.Parse(commandArgs[2]);
                    ShiftValues(list, direction, count);
                    break;
            }
        }

        Console.WriteLine(string.Join(' ', list));
    }

    static void ShiftValues(List<int> list, string direction, int count)
    {
        count %= list.Count;

        if (direction == "left")
        {
            List<int> shiftedList = list.GetRange(0, count);
            list.RemoveRange(0, count);
            list.InsertRange(list.Count, shiftedList);
        }
        else if (direction == "right")
        {
            List<int> shiftedList = list.GetRange(list.Count - count, count);
            list.RemoveRange(list.Count - count, count);
            list.InsertRange(0, shiftedList);
        }
    }

    static bool IsValidIndex(int count, int index)
    {
        return index >= 0 && index < count;
    }
}
