using CarDealership.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Core
{
    public class Controller : IController
    {
        public string AddCustomer(string customerTypeName, string customerName)
        {
            throw new NotImplementedException();
        }

        public string AddVehicle(string vehicleTypeName, string model, double price)
        {
            throw new NotImplementedException();
        }

        public string CustomerReport()
        {
            throw new NotImplementedException();
        }

        public string PurchaseVehicle(string vehicleTypeName, string customerName, double budget)
        {
            throw new NotImplementedException();
        }

        public string SalesReport(string vehicleTypeName)
        {
            throw new NotImplementedException();
        }
    }
}
