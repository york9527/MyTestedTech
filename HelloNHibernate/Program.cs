using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using Xunit;

namespace HelloNHibernate
{
    public class Program
    {
        string connString = "server=.;database=NHibernate;user=sa;pwd=123456";

        static void Main(string[] args)
        {
            
        }

        private void CreateDatabase()
        {
            Fluently.Configure()
                    .Database(MsSqlConfiguration
                                  .MsSql2008
                                  .ConnectionString(connString))
                    .Mappings(m => m.FluentMappings
                                    .AddFromAssemblyOf<ProductMap>())
                    .ExposeConfiguration(CreateSchema)
                    .BuildConfiguration();
        }

        private static void CreateSchema(Configuration cfg)
        {
            var schemaExport = new SchemaExport(cfg);
            schemaExport.Drop(false, true);
            schemaExport.Create(false, true);
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                           .Database(MsSqlConfiguration
                                         .MsSql2008
                                         .ConnectionString(connString))
                           .Mappings(m => m.FluentMappings
                                           .AddFromAssemblyOf<ProductMap>())
                           .BuildSessionFactory();
        }

        /// <summary>
        /// 先手动创建一个空的数据库，然后NHibernate会根据配置单Map，自动生成表。
        /// </summary>
        [Fact]
        void TestCreateDatabase()
        {
            CreateDatabase();
        }

        [Fact]
        void TestCRUDWithNHibernate()
        {
            /*
            var category = new Category()
                {
                    Name = "家电",
                    Description = "电视系列"
                };
            Save(category);
            */

            var category = SearchCategoryByName("家电");
            var c = category.First();
            c.Description = "asdf";
            Update(c);
        }


        private void Save(Category category)
        {
            var factory = CreateSessionFactory();
            using (var session = factory.OpenSession())
            {

                var id = session.Save(category);
            }
        }

        private void Update(Category category)
        {
            using (var session = CreateSessionFactory().OpenSession())
            {
                session.Update(category);
            }
        }

        private void Delete(Category category)
        {
            using (var session = CreateSessionFactory().OpenSession())
            {
                session.Delete(category);
            }
        }

        private IEnumerable<Category> SearchCategoryByName(string name)
        {
            using (var session = CreateSessionFactory().OpenSession())
            {
                return session.Query<Category>().Where(m => m.Name == name).ToList();
            }
        }

    }
}
