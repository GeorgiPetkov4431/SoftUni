using System;

namespace _07._Vending_Machine;

internal class Program
{
    static void Main(string[] args)
    {
        double balance = 0.0;

        string input;

        while ((input = Console.ReadLine()) != "Start") {
            double moneyInput = Convert.ToDouble(input);
            if (moneyInput == 0.1 || moneyInput == 0.2 || moneyInput == 0.5 || moneyInput == 1 || moneyInput == 2) balance += moneyInput;
            else Console.WriteLine($"Cannot accept {moneyInput}");
            //balance += Convert.ToDouble(input);
        }

        while ((input = Console.ReadLine()) != "End") {
            switch (input) {
                case "Nuts":
                    if (balance >= 2.0) {
                        balance -= 2.0;
                        Console.WriteLine("Purchased nuts");
                    }
                    else Console.WriteLine("Sorry, not enough money");
                        break;
                case "Water":
                    if (balance >= 0.7) {
                        balance -= 0.7;
                        Console.WriteLine("Purchased water");
                    }
                    else Console.WriteLine("Sorry, not enough money");
                    break;
                case "Crisps":
                    if (balance >= 1.5) {
                        balance -= 1.5;
                        Console.WriteLine("Purchased crisps");
                    }
                    else Console.WriteLine("Sorry, not enough money");
                    break;
                case "Soda":
                    if (balance >= 0.8) {
                        balance -= 0.8;
                        Console.WriteLine("Purchased soda");
                    }
                    else Console.WriteLine("Sorry, not enough money");
                    break;
                case "Coke":
                    if (balance >= 1.0) {
                        balance -= 1.0;
                        Console.WriteLine("Purchased coke");
                    }
                    else Console.WriteLine("Sorry, not enough money");
                    break;
                default:
                    Console.WriteLine("Invalid product");
                    break;
            }
        }
        Console.WriteLine($"Change: {balance:F2}");
    }
}
