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
            string[] busInfo = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            double carFuelQuantity = double.Parse(carInfo[1]);
            double carFuelConsumption = double.Parse(carInfo[2]);
            double carTankCapacity = double.Parse(carInfo[3]);
            IVehicle car = vehicleFactory.Create(carInfo[0], carFuelQuantity, carFuelConsumption, carTankCapacity);
            vehicles.Add(car);

            double truckFuelQuantity = double.Parse(truckInfo[1]);
            double truckFuelConsumption = double.Parse(truckInfo[2]);
            double truckTankCapacity = double.Parse(truckInfo[3]);
            IVehicle truck = vehicleFactory.Create(truckInfo[0], truckFuelQuantity, truckFuelConsumption, truckTankCapacity);
            vehicles.Add(truck);

            double busFuelQuantity = double.Parse(busInfo[1]);
            double busFuelConsumption = double.Parse(busInfo[2]);
            double busTankCapacity = double.Parse(busInfo[3]);
            IVehicle bus = vehicleFactory.Create(busInfo[0], busFuelQuantity, busFuelConsumption, busTankCapacity);
            vehicles.Add(bus);

            int commandNum = int.Parse(reader.ReadLine());

            for (int i = 0; i < commandNum; i++)
            {
                try
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

                        case "DriveEmpty":
                            if (currentVehicle is Bus currentBus)
                            {
                                double busDistance = double.Parse(commandArgs[2]);
                                writer.WriteLine(currentBus.DriveEmpty(busDistance));
                            }

                            break;

                        case "Refuel":
                            double fuelAmount = double.Parse(commandArgs[2]);
                            currentVehicle.Refuel(fuelAmount);

                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (var vehicle in this.vehicles)
            {
                writer.WriteLine(vehicle.ToString());
            }
        }
    }
}
