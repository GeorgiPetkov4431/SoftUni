using ExplicitInterfaces.Core.Interfaces;
using ExplicitInterfaces.Models;
using ExplicitInterfaces.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplicitInterfaces.Core
{
    public class Engine : IEngine
    {
        public Engine() { }

        public void Run()
        {
            List<Citizen> citizens = new List<Citizen>();

            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                string[] inputArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string name = inputArgs[0];
                string country = inputArgs[1];
                int age = int.Parse(inputArgs[2]);

                citizens.Add(new Citizen(name, country, age));
            }

            foreach(var citizen in citizens)
            {
                Console.WriteLine(((IPerson)citizen).GetName());
                Console.WriteLine(((IResident)citizen).GetName());
            }
        }
    }
}
