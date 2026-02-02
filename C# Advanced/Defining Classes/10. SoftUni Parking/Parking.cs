using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniParking
{
    public class Parking
    {
        private int _capacity;
        private Dictionary<string, Car> _cars;

        public Parking(int capacity)
        {
            this._capacity = capacity;
            this._cars = new Dictionary<string, Car>();
        }

        public int Count {  get { return this._cars.Count; } }

        public string AddCar(Car car)
        {
            if (this._cars.ContainsKey(car.RegistrationNumber))
            {
                return "Car with that registration number, already exists!";
            }
            
            if (this.Count == this._capacity)
            {
                return "Parking is full!";
            }
            
            this._cars.Add(car.RegistrationNumber, car);

            return $"Successfully added new car {car.Make} {car.RegistrationNumber}";
        }

        public string RemoveCar(string registrationNumber)
        {
            /*Car car = _cars.FirstOrDefault(c => c.RegistrationNumber == registrationNumber);

            if (car == null)
            {
                return "Car with that registration number, doesn't exist!";
            }

            _cars.Remove(car);
            */
            if (!this._cars.ContainsKey(registrationNumber))
            {
                return "Car with that registration number, doesn't exist!";
            }

            this._cars.Remove(registrationNumber);
            return $"Successfully removed {registrationNumber}";
        }

        public Car GetCar(string registrationNumber)
        {
            return this._cars[registrationNumber];
        }

        public void RemoveSetOfRegistrationNumbers(List<string> registrationNumbers)
        {
            foreach (string regNum in registrationNumbers)
            {
                RemoveCar(regNum);
            }
        }
    }
}
