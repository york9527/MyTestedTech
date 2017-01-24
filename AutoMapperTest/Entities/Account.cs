using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperTest.Entities
{
    public class Account
    {
        public long Id { get; set; }
        public string AccountName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public decimal PointBalance { get; set; }
        public Address Address { get; set; }
    }
}
