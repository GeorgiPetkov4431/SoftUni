using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Order_by_Age;

internal class Program
{
    static void Main(string[] args)
    {
        List<Person> people = new List<Person>();

        string command;
        while ((command = Console.ReadLine()) != "End")
        {
            string[] commandArgs = command.Split();

            string name = commandArgs[0];
            string id = commandArgs[1];
            int age = int.Parse(commandArgs[2]);

            Person personFound = people.FirstOrDefault(p => p.ID == id);

            if (personFound != null)
            {
                personFound.Name = name;
                personFound.Age = age;
            }
            else people.Add(new Person(name, id, age));
        }

        people = people.OrderBy(p => p.Age).ToList();

        Console.WriteLine(string.Join("\n", people));
    }
}

class Person
{
    public string Name { get; set; }
    public string ID { get; set; }
    public int Age { get; set; }

    public Person(string name, string id, int age)
    {
        Name = name;
        ID = id;
        Age = age;
    }

    public override string ToString()
    {
        return $"{Name} with ID: {ID} is {Age} years old.";
    }
}
