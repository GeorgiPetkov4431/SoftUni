using System;
using System.Collections.Generic;
using System.Transactions;

namespace MoneyTransactions
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] bankAccountsData = Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries);

            Dictionary<int, double> bankAccounts = new Dictionary<int, double>();

            foreach (string bankAccount in bankAccountsData)
            {
                string[] parts = bankAccount.Split("-", StringSplitOptions.RemoveEmptyEntries);

                int accountNumber = int.Parse(parts[0]);
                double accountBalance = double.Parse(parts[1]);

                bankAccounts[accountNumber] = accountBalance;
            }

            while (true)
            {
                try
                {
                    string command = Console.ReadLine();
                    if (command == "End")
                    {
                        break;
                    }

                    string[] commandArgs = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    string commandType = commandArgs[0];
                    int accountNumber = int.Parse(commandArgs[1]);
                    double sum = double.Parse(commandArgs[2]);

                    if (commandType == "Deposit")
                    {
                        bankAccounts[accountNumber] += sum;
                    }
                    else if (commandType == "Withdraw")
                    {
                        if (bankAccounts[accountNumber] < sum)
                        {
                            throw new Exception("Insufficient balance!");
                        }

                        bankAccounts[accountNumber] -= sum;
                    }
                    else
                    {
                        throw new Exception("Invalid command!");
                    }

                    Console.WriteLine($"Account {accountNumber} has new balance: {bankAccounts[accountNumber]:F2}");
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("Invalid account!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                Console.WriteLine("Enter another command");
            }
        }
    }
}
