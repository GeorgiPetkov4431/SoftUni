using CarDealership.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public abstract class Vehicle : IVehicle
    {
        public string Model => throw new NotImplementedException();

        public double Price => throw new NotImplementedException();

        public IReadOnlyCollection<string> Buyers => throw new NotImplementedException();

        public int SalesCount => throw new NotImplementedException();

        public void SellVehicle(string buyerName)
        {
            throw new NotImplementedException();
        }
    }
}
