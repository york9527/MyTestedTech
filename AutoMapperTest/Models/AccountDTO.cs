using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapperTest.Entities;

namespace AutoMapperTest.Models
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string RealName { get; set; }

        public decimal PointBalance { get; set; }
        public Address Address { get; set; }
    }
}
