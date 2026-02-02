using System;
using System.Reflection;
using System.Linq;
using DefiningClasses;
using System.Collections.Generic;

public class StartUp
{
    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
       Family familyList = new Family();
        for (int i = 0; i < n; i++)
        {
            string[] currentPerson = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string currentName = currentPerson[0];
            int currentAge = int.Parse(currentPerson[1]);

            Person person = new Person();
            person.Name = currentName;
            person.Age = currentAge;
            familyList.AddMember(person);
        }
       
        Person oldestPerson = familyList.GetOldestMember();
        Console.WriteLine($"{oldestPerson.Name} {oldestPerson.Age}");
    }
}

