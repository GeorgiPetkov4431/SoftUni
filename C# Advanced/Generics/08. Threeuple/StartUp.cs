using System;

namespace Threeuple
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            string[] nameAndAddressValues = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string[] nameAndBeerValues = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string[] nameAndBankValues = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            CustomThreeuple<string, string, string> nameAndAddress;

            if (nameAndAddressValues.Length > 4)
            {
                nameAndAddress = new CustomThreeuple<string, string, string>($"{nameAndAddressValues[0]} {nameAndAddressValues[1]}", nameAndAddressValues[2], $"{nameAndAddressValues[3]} {nameAndAddressValues[4]}");
            }
            else
            {
                nameAndAddress = new CustomThreeuple<string, string, string>($"{nameAndAddressValues[0]} {nameAndAddressValues[1]}", nameAndAddressValues[2], nameAndAddressValues[3]);
            }

            CustomThreeuple<string, int, bool> nameAndBeer = new CustomThreeuple<string, int, bool>(nameAndBeerValues[0], int.Parse(nameAndBeerValues[1]), nameAndBeerValues[2] == "drunk");

            CustomThreeuple<string, double, string> nameAndBank = new CustomThreeuple<string, double, string>(nameAndBankValues[0], double.Parse(nameAndBankValues[1]), nameAndBankValues[2]);


            Console.WriteLine(nameAndAddress);
            Console.WriteLine(nameAndBeer);
            Console.WriteLine(nameAndBank);
        }
    }
}
