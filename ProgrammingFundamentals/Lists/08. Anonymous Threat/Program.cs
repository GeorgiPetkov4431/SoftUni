using System;
using System.Collections.Generic;
using System.Linq;

namespace _08._Anonymous_Threat;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> list = Console.ReadLine().Split().ToList();

        string command;
        while ((command = Console.ReadLine()) != "3:1")
        {
            string[] commandArgs = command.Split();

            switch (commandArgs[0])
            {
                case "merge":
                    int startIndex = int.Parse(commandArgs[1]);
                    int endIndex = int.Parse(commandArgs[2]);
                    list = Merge(list, startIndex, endIndex);
                    break;
                case "divide":
                    int index = int.Parse(commandArgs[1]);
                    int partitions = int.Parse(commandArgs[2]);
                    list = Divide(list, index, partitions);
                    break;
            }
        }

        Console.WriteLine(string.Join(' ', list));
    }

    static List<string> Merge(List<string> list, int startIndex, int endIndex)
    {
        startIndex = Clamp(startIndex, 0, list.Count - 1);
        endIndex = Clamp(endIndex, 0, list.Count - 1);

        List<string> range = list.GetRange(startIndex, endIndex - startIndex + 1);
        string mergedStr = string.Join(string.Empty, range);
        list.RemoveRange(startIndex, endIndex - startIndex + 1);
        list.Insert(startIndex, mergedStr);

        return list;
    }

    static List<string> Divide(List<string> list, int index, int partitions)
    {
        if (partitions <= 0 || index < 0 || index >= list.Count) return list;

        string element = list[index];
        List<string> newElements = new();

        int partitionLength = element.Length / partitions;
        int remainingChars = element.Length % partitions;

        int elementIndex = 0;
        for (int i = 0; i < partitions; i++)
        {
            string newString = string.Empty;
            for (int j = 0; j < partitionLength; j++) newString += element[elementIndex++];
            newElements.Add(newString);
        }

        if (remainingChars > 0 && newElements.Count > 0)
        {
            for (int i = elementIndex; i < element.Length; i++)
            {
                newElements[newElements.Count - 1] += element[i];
            }
        }

        list.RemoveRange(index, 1);
        list.InsertRange(index, newElements);

        return list;
    }

    static int Clamp(int index, int min, int max)
    {
        if (index < min) index = min;
        else if (index > max) index = max;
        return index;
    }
}