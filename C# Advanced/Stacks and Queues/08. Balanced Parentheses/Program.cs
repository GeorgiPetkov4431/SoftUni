using System;
using System.Collections.Generic;
using System.Linq;

namespace _08._Balanced_Parentheses;

internal class Program
{
    static void Main(string[] args)
    {
        string? input = Console.ReadLine();
        //string input = Console.ReadLine() ?? string.Empty;

        if (input is null)
        {
            Console.WriteLine("NO");
            return;
        }

        Stack<char> stack = new Stack<char>();
        bool isBalanced = true;

        foreach (char c in input)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else
            {
                if (!stack.Any())
                {
                    isBalanced = false;
                    break;
                }

                char bracket = stack.Pop();
                if ((bracket == '(' && c == ')') || (bracket == '[' && c == ']') || (bracket == '{' && c == '}'))
                {
                    continue;
                }
                else
                {
                    isBalanced = false;
                    break;
                }
            }
        }

        Console.WriteLine(isBalanced ? "YES" : "NO");
    }
}
