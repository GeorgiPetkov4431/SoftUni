using System;

namespace Tuple
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            string[] nameAndAddressValues = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string[] nameAndBeerValues = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string[] intAndDoubleValues = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            CustomTuple<string, string> nameAndAddress = new CustomTuple<string, string>($"{nameAndAddressValues[0]} {nameAndAddressValues[1]}", nameAndAddressValues[2]);
            CustomTuple<string, int> nameAndBeer = new CustomTuple<string, int>(nameAndBeerValues[0], int.Parse(nameAndBeerValues[1]));
            CustomTuple<int, double> intAndDouble = new CustomTuple<int, double>(int.Parse(intAndDoubleValues[0]), double.Parse(intAndDoubleValues[1]));

            Console.WriteLine(nameAndAddress);
            Console.WriteLine(nameAndBeer);
            Console.WriteLine(intAndDouble);
        }
    }
}
