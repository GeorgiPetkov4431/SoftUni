using _06._Equality_Logic;
using System;
using System.Collections.Generic;

namespace EqualityLogic
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            HashSet<Person> peopleSet = new HashSet<Person>();

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(' ');
                Person person = new Person(input[0], int.Parse(input[1]));
                peopleSet.Add(person);
            }

            Console.WriteLine(peopleSet.Count);
            Console.WriteLine(peopleSet.Count);
        }
    }
}
