using BirthdayCelebrations.Models;
using BirthdayCelebrations.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BirthdayCelebrations
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IBirthable> birthables = new List<IBirthable>();

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] commandArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string type = commandArgs[0];
                string name = commandArgs[1];
                

                IBirthable birthable;

                switch (type)
                {
                    case "Citizen":
                        int age = int.Parse(commandArgs[2]);
                        string id = commandArgs[3];
                        string citizenBirthday = commandArgs[4];
                        birthable = new Citizen(name, age, id, citizenBirthday);
                        birthables.Add(birthable);
                        break;

                    case "Pet":
                        string petBirthday = commandArgs[2];
                        birthable = new Pet(name, petBirthday);
                        birthables.Add(birthable);
                        break;
                }
            }

            string yearFilter = Console.ReadLine();

            foreach (var birthable in birthables.Where(bir => bir.BirthDate.EndsWith(yearFilter)))
            {
                Console.WriteLine(birthable.BirthDate);
            }
        }
    }
}
