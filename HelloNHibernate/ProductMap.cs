using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace HelloNHibernate
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.UnitPrice);
            Map(x => x.ReorderLevel);
            Map(x => x.Discontinued);
            References(x => x.Category);
        }
    }
}
