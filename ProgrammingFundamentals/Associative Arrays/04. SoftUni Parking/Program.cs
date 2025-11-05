using System;
using System.Linq;
using System.Collections.Generic;

namespace _04._SoftUni_Parking;

internal class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, Driver> drivers = new Dictionary<string, Driver>();

        int commandsCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < commandsCount; i++)
        {
            string[] command = Console.ReadLine().Split();

            string username = command[1];

            switch (command[0])
            {
                case "register":
                    string licensePlateNumber = command[2];
                    Driver newDriver = new Driver(username, licensePlateNumber);
                    if (!drivers.ContainsKey(username))
                    {
                        drivers.Add(username, newDriver);
                        Console.WriteLine($"{username} registered {licensePlateNumber} successfully");
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: already registered with plate number {licensePlateNumber}");
                    }

                    break;

                case "unregister":
                    if (!drivers.ContainsKey(username))
                    {
                        Console.WriteLine($"ERROR: user {username} not found");
                    }
                    else
                    {
                        drivers.Remove(username);
                        Console.WriteLine($"{username} unregistered successfully");
                    }

                    break;
            }
        }

        foreach (KeyValuePair<string, Driver> driver in drivers)
        {
            Console.WriteLine(driver.Value);
        }
    }
}

class Driver
{
    public string Username { get; set; }
    public string LicensePlateNumber { get; set; }
    public Driver(string username, string licensePlateNumber)
    {
        Username = username;
        LicensePlateNumber = licensePlateNumber;
    }

    public override string ToString()
    {
        return $"{Username} => {LicensePlateNumber}";
    }
}
