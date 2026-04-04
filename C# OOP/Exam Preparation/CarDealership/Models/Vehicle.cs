using CarDealership.Models.Contracts;
using CarDealership.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string model;
        private double price;
        private readonly List<string> buyers;

        public Vehicle(string model, double price)
        {
            this.Model = model;
            this.Price = price;
            this.buyers = new List<string>();
        }

        public string Model
        { 
            get { return this.model; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelIsRequired);
                }
                this.model = value;
            }
        }

        public double Price
        {
            get { return this.price; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.PriceMustBePositive);
                }
                this.price = value;
            }
        }

        public IReadOnlyCollection<string> Buyers { get { return this.buyers.AsReadOnly(); } }

        public int SalesCount { get { return this.buyers.Count; } }

        public void SellVehicle(string buyerName)
        {
            buyers.Add(buyerName);
        }

        public override string ToString()
        {
            return $"{Model} - Price: {Price:F2}, Total Model Sales: {SalesCount}";
        }
    }
}
