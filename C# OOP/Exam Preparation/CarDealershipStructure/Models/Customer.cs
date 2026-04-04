using CarDealership.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public abstract class Customer : ICustomer
    {
        public string Name => throw new NotImplementedException();

        public IReadOnlyCollection<string> Purchases => throw new NotImplementedException();

        public void BuyVehicle(string vehicleModel)
        {
            throw new NotImplementedException();
        }
    }
}
