using CarDealership.Models.Contracts;
using CarDealership.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private readonly List<IVehicle> vehicles;

        public VehicleRepository()
        {
            vehicles = new List<IVehicle>();
        }

        public IReadOnlyCollection<IVehicle> Models { get { return this.vehicles.AsReadOnly(); } }

        public void Add(IVehicle vehicle)
        {
            vehicles.Add(vehicle);
        }

        public bool Exists(string text)
        {
            return vehicles.Any(m => m.Model == text);
        }

        public IVehicle Get(string model)
        {
            return vehicles.FirstOrDefault(v => v.Model == model);
        }

        public bool Remove(string text)
        {
            IVehicle vehicle = vehicles.FirstOrDefault(m => m.Model == text);

            bool result = vehicles.Remove(vehicle);
            return result;
        }
    }
}
