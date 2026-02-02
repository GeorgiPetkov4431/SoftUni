using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            List<Car> cars = new List<Car>();

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string model = input[0];
                double fuelAmount = double.Parse(input[1]);
                double fuelConsumption = double.Parse(input[2]);

                cars.Add(new Car(model, fuelAmount, fuelConsumption));
            }

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] commandArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string carModel = commandArgs[1];
                double distance = double.Parse(commandArgs[2]);

                Car car = cars.First(c => c.Model == carModel);
                car.Drive(distance);
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }
    }
}
