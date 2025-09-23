using System;

namespace _06._Triples_of_Latin_Letters;

internal class Program
{
    static void Main(string[] args)
    {
        int value = int.Parse(Console.ReadLine());

        //first solution
        for (int i = 'a'; i < ('a' + value); i++) {
            for (int j = 'a'; j < ('a' + value); j++) {
                for (int k = 'a'; k < ('a' + value); k++) {
                    Console.WriteLine($"{(char)i}{(char)j}{(char)k}");
                }
            }
        }

        Console.WriteLine();

        //second solution
        for (int i = 0; i < value; i++) {
            char firstChar = (char)('a' + i);
            for (int j = 0; j < value; j++) {
                char secondChar = (char)('a' + j);
                for (int k = 0; k < value; k++) {
                    char thirdChar = (char)('a' + k);
                    Console.WriteLine($"{firstChar}{secondChar}{thirdChar}");
                }
            }
        }
    }
}
