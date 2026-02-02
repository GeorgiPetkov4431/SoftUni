using System;
using System.Collections.Specialized;
using System.Linq;

namespace CarSalesman
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            Engine[] engines = new Engine[n];

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string model = input[0];
                int power = int.Parse(input[1]);
                int? displacement = null;
                string efficiency = null;
                if (input.Length == 3)
                {
                    int displacementParsed;
                    if (int.TryParse(input[2], out displacementParsed))
                    {
                        displacement = displacementParsed;
                    }
                    else
                    {
                        efficiency = input[2];
                    }
                }
                if (input.Length == 4)
                {
                    displacement = int.Parse(input[2]);
                    efficiency = input[3];
                }

                engines[i] = new Engine(model, power, displacement, efficiency);
            }

            int m = int.Parse(Console.ReadLine());

            Car[] cars = new Car[m];

            for (int i = 0; i < m; i++)
            {
                string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string model = input[0];
                Engine engine = engines.First(e => e.Model == input[1]);
                int? weight = null;
                string color = null;
                if (input.Length >= 3)
                {
                    int weightParsed;
                    if (int.TryParse(input[2], out weightParsed))
                    {
                        weight = weightParsed;
                    }
                    else
                    {
                        color = input[2];
                    }
                }
                if (input.Length == 4)
                {
                    weight = int.Parse(input[2]);
                    color = input[3];
                }

                cars[i] = new Car(model, engine, weight, color);
            }

            foreach (Car car in cars)
            {
                Console.WriteLine(car);
            }
        }
    }
}
