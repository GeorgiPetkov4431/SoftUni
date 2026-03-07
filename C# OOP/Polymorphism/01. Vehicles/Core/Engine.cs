using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Core.Interfaces;
using Vehicles.Factories.Interfaces;
using Vehicles.IO.Interfaces;
using Vehicles.Models;
using Vehicles.Models.Interfaces;

namespace Vehicles.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IVehicleFactory vehicleFactory;

        private readonly ICollection<IVehicle> vehicles;

        public Engine(IReader reader, IWriter writer, IVehicleFactory vehicleFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.vehicleFactory = vehicleFactory;

            this.vehicles = new List<IVehicle>();
        }

        public void Run()
        {
            string[] carInfo = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] truckInfo = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            double carFuelQuantity = double.Parse(carInfo[1]);
            double carFuelConsumption = double.Parse(carInfo[2]);
            IVehicle car = vehicleFactory.Create(carInfo[0], carFuelQuantity, carFuelConsumption);
            vehicles.Add(car);

            double truckFuelQuantity = double.Parse(truckInfo[1]);
            double truckFuelConsumption = double.Parse(truckInfo[2]);
            IVehicle truck = vehicleFactory.Create(truckInfo[0], truckFuelQuantity, truckFuelConsumption);
            vehicles.Add(truck);

            int commandNum = int.Parse(reader.ReadLine());

            for (int i = 0; i < commandNum; i++)
            {
                string[] commandArgs = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string commandType = commandArgs[0];
                string vehicleType = commandArgs[1];

                IVehicle currentVehicle = this.vehicles.FirstOrDefault(v => v.GetType().Name == vehicleType);

                switch (commandType)
                {
                    case "Drive":
                        double distance = double.Parse(commandArgs[2]);
                        writer.WriteLine(currentVehicle.Drive(distance));
                        break;
                    case "Refuel":
                        double fuelAmount = double.Parse(commandArgs[2]);
                        currentVehicle.Refuel(fuelAmount);
                        break;
                }
            }

            foreach (var vehicle in this.vehicles)
            {
                writer.WriteLine(vehicle.ToString());
            }
        }
    }
}
