using System;

namespace _09._Palindrome_Integers;

internal class Program
{
    static void Main(string[] args)
    {
        string command;

        while ((command = Console.ReadLine()) != "END")
        {
            Console.WriteLine(CheckIfPalindrome(command));
        }
    }

    static bool CheckIfPalindrome(string number)
    {
        for (int i = 0; i < number.Length / 2; i++)
        {
            if (number[i] != number[number.Length - (1 + i)]) return false;
        }
        return true;
    }
}
