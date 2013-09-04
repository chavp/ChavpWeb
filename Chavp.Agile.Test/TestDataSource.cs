using System;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chavp.Agile.Test
{
    using NHibernate;
    using NHibernate.Linq;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using Chavp.Agile.Entities;
    using Chavp.Agile.Mappings;

    [TestClass]
    public class TestDataSource
    {
        /// <summary>
        /// http://www.codeproject.com/Articles/543810/Dependency-Injection-and-Unit-Of-Work-using-Castle
        /// </summary>
        [TestMethod]
        public void SimpleInsert()
        {
            var sessionFactory = CreateSessionFactory();
            var uow = new UnitOfWork(sessionFactory);
            uow.BeginTransaction();
            UnitOfWork.Current = uow;

            var productRepo = new ProductRepository();

            for (int i = 0; i < 10; i++)
            {
                var p = new Product(string.Format("P-{0}", i))
                {
                };

                productRepo.SaveOrUpdate(p);
            }

            uow.Commit();
        }

        [TestMethod]
        public void SimpleQuery()
        {
            var sessionFactory = CreateSessionFactory();
            var uow = new UnitOfWork(sessionFactory);
            UnitOfWork.Current = uow;
            uow.BeginTransaction();

            var productRepo = new ProductRepository();
            var products = productRepo.All().ToList();

            products.ForEach(p =>
            {
                var f = p.Features.First();
                p.Features.Remove(f);
                //p.AddFeature(new Feature("F-" + p.CodeName));
                //Console.WriteLine(p.CodeName);
            });

            uow.Commit();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                SQLiteConfiguration.Standard
                .UsingFile("agile.db")
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductMap>())
                //.ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            if (File.Exists("agile.db"))
                File.Delete("agile.db");

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
              .Create(false, true);
        }
    }
}
