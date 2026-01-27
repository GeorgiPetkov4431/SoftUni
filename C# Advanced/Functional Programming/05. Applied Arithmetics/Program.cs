using System;
using System.Linq;

namespace _05._Applied_Arithmetics;

internal class Program
{
    static void Main(string[] args)
    {
        int[] nums = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        

        string command;

        while ((command = Console.ReadLine()) != "end")
        {
            Func<int, int> operation = null;
            switch (command)
            {
                case "add":
                    operation = num => num + 1;
                    break;
                case "multiply":
                    operation = num => num * 2;
                    break;
                case "subtract":
                    operation = num => num - 1;
                    break;
                case "print":
                    Console.WriteLine(string.Join(" ", nums));
                    break;
            }
            if (operation != null)
            {
                ManipulateArray(nums, operation);
            }
        }
    }

    static int[] ManipulateArray(int[] nums, Func<int, int> operation)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = operation(nums[i]);
        }

        return nums;
    }
}
