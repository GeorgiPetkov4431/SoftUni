using System;
using PizzaCalories.Models;

namespace PizzaCalories
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            try {
                string input = Console.ReadLine();
                string[] inputArgs = input.Split(' ');
            
                string pizzaName = inputArgs[1];

                input = Console.ReadLine();
                inputArgs = input.Split(' ');

                string flourType = inputArgs[1];
                string bakingTechnique = inputArgs[2];
                double doughWeight = double.Parse(inputArgs[3]);

            
                Dough dough = new Dough(flourType, bakingTechnique, doughWeight);

                Pizza pizza = new Pizza(pizzaName, dough);

                while ((input = Console.ReadLine()) != "END")
                {
                    inputArgs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    string toppingName = inputArgs[1];
                    double toppingWeight = double.Parse(inputArgs[2]);

                    Topping topping = new Topping(toppingName, toppingWeight);

                    pizza.AddTopping(topping);
                }

                Console.WriteLine(pizza);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
