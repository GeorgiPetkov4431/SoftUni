using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Bomb_Numbers;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> list = Console.ReadLine().Split().Select(int.Parse).ToList();
        List<int> bomb = Console.ReadLine().Split().Select(int.Parse).ToList();

        list = Explode(list, bomb);

        Console.WriteLine(Sum(list));
    }

    static int Sum(List<int> list)
    {
        int sum = 0;

        foreach (int item in list) sum += item;
        return sum;
    }

    static List<int> Explode(List<int> list, List<int> bomb)
    {
        int number = bomb[0];
        int power = bomb[1];

        while (list.Contains(number))
        {
            int index = list.IndexOf(number);

            int leftIndex = Math.Max(0, index - power);
            int rightIndex = Math.Min(index + power, list.Count - 1);

            int range = rightIndex - leftIndex + 1;
            list.RemoveRange(leftIndex, range);
        }

        return list;
    }
}
