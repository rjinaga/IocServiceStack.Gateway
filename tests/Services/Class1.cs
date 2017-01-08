using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService : ICustomer
    {
        public Customer GetCustomer(int i, string s)
        {
            return new Customer() { Name = i + s };
        }
        public void Save()
        {

        }
    }
}
