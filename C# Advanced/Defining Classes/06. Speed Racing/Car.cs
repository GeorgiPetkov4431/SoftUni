using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiningClasses
{
    public class Car
    {
        private string _model;
        private double _fuelAmount;
        private double _fuelConsumptionPerKilometer;
        private double _traveledDistance;

        public Car(string model, double fuelAmount, double fuelConsumptionPerKilometer)
        {
            Model = model;
            FuelAmount = fuelAmount;
            FuelConsumptionPerKilometer = fuelConsumptionPerKilometer;
            _traveledDistance = 0;
        }

        public string Model
        {
            get {  return _model; }
            set { _model = value; }
        }

        public double FuelAmount
        {
            get { return _fuelAmount; }
            set { _fuelAmount = value; }
        }

        public double FuelConsumptionPerKilometer
        {
            get { return _fuelConsumptionPerKilometer; }
            set { _fuelConsumptionPerKilometer = value; }
        }

        public double TraveledDistance
        {
            get { return _traveledDistance; }
            set { _traveledDistance = value; }
        }

        public void Drive(double distance)
        {
            double fuelRequired = distance * FuelConsumptionPerKilometer;
            if (fuelRequired > FuelAmount)
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
            else
            {
                FuelAmount -= fuelRequired;
                TraveledDistance += distance;
            }
        }

        public override string ToString()
        {
            return $"{Model} {FuelAmount:F2} {TraveledDistance}";
        }
    }
}
