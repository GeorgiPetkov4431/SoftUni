using CarDealership.Models.Contracts;
using CarDealership.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Repositories
{
    public class CustomerRepository : IRepository<ICustomer>
    {
        private readonly List<ICustomer> customers;

        public CustomerRepository()
        {
            customers = new List<ICustomer>();
        }

        public IReadOnlyCollection<ICustomer> Models { get { return this.customers.AsReadOnly(); } }

        public void Add(ICustomer model)
        {
            customers.Add(model);
        }

        public bool Exists(string text)
        {
            return Models.Any(m => m.Name == text);
        }

        public ICustomer Get(string text)
        {
            return Models.FirstOrDefault(m => m.Name == text);
        }

        public bool Remove(string text)
        {
            ICustomer customer = Models.FirstOrDefault(m => m.Name == text);

            return customers.Remove(customer);
        }
    }
}
