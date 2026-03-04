using FoodShortage.Models;
using FoodShortage.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodShortage
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IBuyer> buyers = new List<IBuyer>();

            int count = int.Parse(Console.ReadLine());

            string input;
            for (int i = 0;  i < count; i++)
            {
                input = Console.ReadLine();
                string[] inputArgs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                IBuyer buyer;

                if (inputArgs.Length == 4)
                {
                    buyer = new Citizen(inputArgs[0], int.Parse(inputArgs[1]), inputArgs[2], inputArgs[3]);
                }
                else
                {
                    buyer = new Rebel(inputArgs[0], int.Parse(inputArgs[1]), inputArgs[2]);
                }

                buyers.Add(buyer);
            }

            while ((input = Console.ReadLine()) != "End")
            {
                IBuyer buyer = buyers.FirstOrDefault(b => b.Name == input);
                if (buyer is not null)
                {
                    buyer.BuyFood();
                }
            }

            Console.WriteLine(buyers.Sum(b => b.Food));
        }
    }
}
