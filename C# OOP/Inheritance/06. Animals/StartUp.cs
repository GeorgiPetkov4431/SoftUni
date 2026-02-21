using System;
using System.Collections.Generic;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();

            string input;

            while ((input = Console.ReadLine()) != "Beast!")
            {
                string type = input;
                string[] animalDetails = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string name = animalDetails[0];
                int age = int.Parse(animalDetails[1]);
                string gender = animalDetails[2];

                Animal animal = null;
                try
                {
                    switch (type)
                    {
                        case "Cat":
                            animal = new Cat(name, age, gender);
                            break;
                        case "Dog":
                            animal = new Dog(name, age, gender);
                            break;
                        case "Frog":
                            animal = new Frog(name, age, gender);
                            break;
                        case "Kitten":
                            animal = new Kitten(name, age);
                            break;
                        case "Tom":
                            animal = new Tomcat(name, age);
                            break;
                    }

                    animals.Add(animal);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}
