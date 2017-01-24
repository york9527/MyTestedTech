using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperTest.Entities
{
    public class Address
    {
        public Area City { get; set; }
        public Area Province { get; set; }
        public string AddressDetail{get;set;}
    }
}
