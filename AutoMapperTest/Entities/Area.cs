﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperTest.Entities
{
    public class Area
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public int AreaParentId { get; set; }
    }
}
