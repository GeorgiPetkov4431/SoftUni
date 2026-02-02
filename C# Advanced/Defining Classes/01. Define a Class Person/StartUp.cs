using System;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Person person1 = new Person();
            person1.Name = "Georgi";
            person1.Age = 23;

            Person person2 = new Person
            {
                Name = "Peter",
                Age = 21
            };
        }
    }
}
