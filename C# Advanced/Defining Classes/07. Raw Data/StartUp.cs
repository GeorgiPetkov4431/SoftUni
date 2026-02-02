using System;
using System.Linq;

namespace RawData {

    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            Car[] cars = new Car[n];

            //"{model} {engineSpeed} {enginePower} {cargoWeight} {cargoType} {tire1Pressure}
            //{tire1Age} {tire2Pressure} {tire2Age} {tire3Pressure} {tire3Age} {tire4Pressure} {tire4Age}"

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string model = input[0];
                double engineSpeed = double.Parse(input[1]);
                double enginePower = double.Parse(input[2]);
                double cargoWeight = double.Parse(input[3]);
                string cargoType = input[4];
                double tire1Pressure = double.Parse(input[5]);
                int tire1Age = int.Parse(input[6]);
                double tire2Pressure = double.Parse(input[7]);
                int tire2Age = int.Parse(input[8]);
                double tire3Pressure = double.Parse(input[9]);
                int tire3Age = int.Parse(input[10]);
                double tire4Pressure = double.Parse(input[11]);
                int tire4Age = int.Parse(input[12]);

                Engine engine = new Engine(engineSpeed, enginePower);
                Cargo cargo = new Cargo(cargoType, cargoWeight);
                Tire[] tires =
                [
                    new Tire(tire1Age, tire1Pressure),
                    new Tire(tire2Age, tire2Pressure),
                    new Tire(tire3Age, tire3Pressure),
                    new Tire(tire4Age, tire4Pressure),
                ];

                cars[i] = new Car(model, engine, cargo, tires);
            }

            string cargoTypeFilter = Console.ReadLine();

            Car[] filteredCars;

            if (cargoTypeFilter == "fragile")
            {
                filteredCars = cars.Where(c => c.Cargo.Type == "fragile" && c.TireUnderPressure()).ToArray();

            }
            else
            {
                filteredCars = cars.Where(c => c.Cargo.Type == "flammable" && c.Engine.Power > 250).ToArray();
            }

            foreach (Car car in filteredCars)
            {
                Console.WriteLine(car.Model);
            }
        }
    }
}