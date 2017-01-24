using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using AutoMapperTest.Entities;
using AutoMapperTest.Models;

namespace AutoMapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var account = new Account()
            {
                Id = 111,
                AccountName = "werwr@gmail.com",
                FirstName = "张",
                MiddleName = "无",
                LastName = "忌",
                PointBalance = 123.23m,
                Address = new Address()
                {
                    City = new Area()
                    {
                        Id = 1916,
                        AreaName = "深圳市",
                        AreaParentId = 1900
                    },
                    Province = new Area()
                    {
                        Id = 1920,
                        AreaName = "广东省",
                        AreaParentId = 1916
                    },
                    AddressDetail = "某某大道某某号"
                }
            };
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Account, AccountDTO>()
                .ForMember(m => m.RealName, opt => opt.MapFrom(ol => ol.FirstName + " " + ol.LastName));

            });
            var mapper = config.CreateMapper();
            var accountDto = mapper.Map<Account, AccountDTO>(account);
                

            Console.ReadLine();

        }
    }
}
