using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models
{
    public class Bus : Vehicle
    {
        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public override double FuelConsumption { get { return base.FuelConsumption + 1.4; } }

        public string DriveEmpty(double distance)
        {
            base.FuelConsumption -= 1.4;

            return Drive(distance);
        }
    }
}
