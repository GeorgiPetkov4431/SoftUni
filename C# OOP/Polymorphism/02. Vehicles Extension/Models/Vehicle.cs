using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models.Interfaces;

namespace Vehicles.Models
{
    public abstract class Vehicle : IVehicle
    {
        private double fuelQuantity;

        protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            this.TankCapacity = tankCapacity;
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption;
        }

        public double FuelQuantity
        {
            get { return this.fuelQuantity; }
            set
            {
                if (this.TankCapacity < value)
                {
                    this.fuelQuantity = 0;
                }
                else
                {
                    this.fuelQuantity = value;
                }
            }
        }

        public virtual double FuelConsumption { get; protected set; }

        public double TankCapacity { get; private set; }

        public string Drive(double distance)
        {
            string vehicleType = this.GetType().Name;

            if (this.FuelQuantity < distance * this.FuelConsumption)
            {
                return $"{vehicleType} needs refueling";
            }

            this.FuelQuantity -= distance * this.FuelConsumption;
            return $"{vehicleType} travelled {distance} km";
        }

        public virtual void Refuel(double fuel)
        {
            if (this.TankCapacity < this.FuelQuantity + fuel)
            {
                throw new ArgumentException($"Cannot fit {fuel} fuel in the tank");
            }

            if (fuel <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }

            this.FuelQuantity += fuel;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.FuelQuantity:F2}";
        }
    }
}
