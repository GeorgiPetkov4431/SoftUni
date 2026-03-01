using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingSpree.Models;


namespace ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            List<Product> products = new List<Product>();

            try
            {
                string[] peopleValues = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);

                foreach (string person in peopleValues)
                {
                    string[] personValues = person.Split('=', StringSplitOptions.RemoveEmptyEntries);

                    string name = personValues[0];
                    decimal money = decimal.Parse(personValues[1]);

                    people.Add(new Person(name, money));
                }

                string[] productsValues = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);

                foreach (string product in productsValues)
                {
                    string[] productValues = product.Split('=', StringSplitOptions.RemoveEmptyEntries);

                    string name = productValues[0];
                    decimal cost = decimal.Parse(productValues[1]);

                    products.Add(new Product(name, cost));
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);

                return;
            }

            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] commandArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string personName = commandArgs[0];
                string productName = commandArgs[1];

                Person person = people.FirstOrDefault(p => p.Name == personName);
                Product product = products.FirstOrDefault(p => p.Name == productName);

                if (person is not null && product is not null)
                {
                    Console.WriteLine(person.Add(product));
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, people));
        }
    }
}
