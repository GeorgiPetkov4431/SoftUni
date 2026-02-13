using System;
using System.Collections.Generic;

namespace ComparingObjects
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();

            string command;

            while ((command = Console.ReadLine()) != "END")
            {
                string[] commandArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string name = commandArgs[0];
                int age = int.Parse(commandArgs[1]);
                string town = commandArgs[2];

                people.Add(new Person(name, age, town));
            }

            int n = int.Parse(Console.ReadLine());

            Person chosenPerson = people[n - 1];
            people.RemoveAt(n - 1);

            int matches = 0;

            foreach (var person in people)
            {
                if (chosenPerson.CompareTo(person) == 0)
                {
                    matches++;
                }
            }

            string output = matches == 0 ? "No matches" : $"{matches + 1} {people.Count - matches} {people.Count + 1}";

            Console.WriteLine(output);
        }
    }
}
