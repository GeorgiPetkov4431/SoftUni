using System;
using System.Linq;

namespace _11._Array_Manipulator;

internal class Program
{
    static void Main(string[] args)
    {
        int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

        string command;

        while ((command = Console.ReadLine()) != "end")
        {
            string[] commandArr = command.Split();

            switch (commandArr[0]) {
                case "exchange":
                    int index = int.Parse(commandArr[1]);
                    numbers = ExchangeElements(numbers, index);
                    break;
                case "max":
                    PrintMaxIndex(numbers, commandArr[1]);
                    break;
                case "min":
                    PrintMinIndex(numbers, commandArr[1]);
                    break;
                case "first":
                    int firstCount = int.Parse(commandArr[1]);
                    string firstType = commandArr[2];
                    PrintFirstElements(numbers, firstCount, firstType);
                    break;
                case "last":
                    int lastCount = int.Parse(commandArr[1]);
                    string lastType = commandArr[2];
                    PrintLastElements(numbers, lastCount, lastType);
                    break;
            }
        }

        Console.WriteLine($"[{string.Join(", ", numbers)}]");
    }

    static int[] ExchangeElements(int[] numbers, int index)
    {
        if (CheckIfIndexOutOfRange(numbers, index))
        {
            Console.WriteLine("Invalid index");
            return numbers;
        }

        int[] newArray = new int[numbers.Length];
        int newArrayIndex = 0;

        for (int i = index + 1; i < numbers.Length; i++)
        {
            newArray[newArrayIndex++] = numbers[i];
        }

        for (int i = 0; i <= index; i++)
        {
            newArray[newArrayIndex++] = numbers[i];
        }

        return newArray;
    }

    static void PrintMaxIndex(int[] numbers, string type)
    {
        int maxIndex = -1;
        int maxNum = int.MinValue;
        int expectedResult = 2;
        if (type == "even") expectedResult = 0;
        else if (type == "odd") expectedResult = 1;

        for (int i = 0; i < numbers.Length; ++i) {
            if (numbers[i] >= maxNum && numbers[i] % 2 == expectedResult) {
                maxNum = numbers[i];
                maxIndex = i;
            }
        }

        PrintDefaultIndex(maxIndex);
    }

    static void PrintMinIndex(int[] numbers, string type)
    {
        int minIndex = -1;
        int minNum = int.MaxValue;
        int expectedResult = 2;
        if (type == "even") expectedResult = 0;
        else if (type == "odd") expectedResult = 1;

        for (int i = 0; i < numbers.Length; ++i)
        {
            if (numbers[i] <= minNum && numbers[i] % 2 == expectedResult)
            {
                minNum = numbers[i];
                minIndex = i;
            }
        }

        PrintDefaultIndex(minIndex);
    }

    static void PrintFirstElements(int[] numbers, int count, string type)
    {
        if (count > numbers.Length)
        {
            Console.WriteLine("Invalid count");
            return;
        }

        int expectedResult = 2;
        int elementCount = 0;
        string firstElements = string.Empty;

        if (type == "even") expectedResult = 0;
        else if (type == "odd") expectedResult = 1;

        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] % 2 == expectedResult)
            {
                firstElements += $"{numbers[i]}, ";
                elementCount++;
            }
            if (elementCount >= count) break;
        }

        Console.WriteLine($"[{firstElements.Trim(' ', ',')}]");
    }

    static void PrintLastElements(int[] numbers, int count, string type)
    {
        if (count > numbers.Length)
        {
            Console.WriteLine("Invalid count");
            return;
        }

        int expectedResult = 2;
        int elementCount = 0;
        string lastElements = string.Empty;

        if (type == "even") expectedResult = 0;
        else if (type == "odd") expectedResult = 1;

        for (int i = numbers.Length - 1; i >= 0; i--)
        {
            if (numbers[i] % 2 == expectedResult)
            {
                lastElements = $"{numbers[i]}, " + lastElements;
                elementCount++;
            }
            if (elementCount >= count) break;
        }

        Console.WriteLine($"[{lastElements.Trim(' ',',')}]");
    }

    static void PrintDefaultIndex(int index)
    {
        if (index != -1) Console.WriteLine(index);
        else Console.WriteLine("No matches");
    }

    static bool CheckIfIndexOutOfRange(int[] numbers, int index)
    {
        return (index < 0 || index >= numbers.Length);
    }
}
