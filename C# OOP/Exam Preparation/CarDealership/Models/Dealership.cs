using CarDealership.Models.Contracts;
using CarDealership.Repositories;
using CarDealership.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public class Dealership : IDealership
    {
        private readonly IRepository<IVehicle> vehicleRepository;
        private readonly IRepository<ICustomer> customerRepository;

        public Dealership()
        {
            vehicleRepository = new VehicleRepository();
            customerRepository = new CustomerRepository();
        }

        public IRepository<IVehicle> Vehicles { get { return this.vehicleRepository; } }

        public IRepository<ICustomer> Customers { get { return this.customerRepository; } }
    }
}
