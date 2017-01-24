using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain
{
    public class Customer : Entity<Customer>
    {
        public string CustomerIdentifier { get; private set; }
        public Name CustomerName { get; private set; }

        public void ChangeCustomerName(string firstName, string middleName, string lastName)
        {
            CustomerName = new Name(firstName, middleName, lastName);
        }
    }
}
