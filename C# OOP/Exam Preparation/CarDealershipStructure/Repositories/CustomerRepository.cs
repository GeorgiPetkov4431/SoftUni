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
        public IReadOnlyCollection<ICustomer> Models => throw new NotImplementedException();

        public void Add(ICustomer model)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string text)
        {
            throw new NotImplementedException();
        }

        public ICustomer Get(string text)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string text)
        {
            throw new NotImplementedException();
        }
    }
}
