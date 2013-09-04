using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Web.Installers
{
    using Plumbing;
    using Castle.Core;
    using Web.Interceptors;
    using Chavp.Agile.Entities;
    using Chavp.Agile.Mappings;
    using NHibernate.Tool.hbm2ddl;
    using System.IO;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using System.Web.Hosting;
    using Chavp.Agile.Entities.Attributes;

    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            container.Register(
                Classes.
                    FromThisAssembly().
                    BasedOn<IController>().
                    If(c => c.Name.EndsWith("Controller")).
                    LifestyleTransient(),

                // Service
                Component.For<IProductService>()
                .ImplementedBy<ProductService>()
                .LifeStyle.Transient,

                // Nhibernate session factory
                Component.For<NHibernate.ISessionFactory>()
                .Instance(OrmSessionFactory.CreateSessionFactory())
                .LifeStyle.Singleton,

                // Unitofwork interceptor
                Component.For<UnitOfWorkInterceptor>().LifeStyle.Transient,

                Component.For<IProductRepository>()
                .ImplementedBy<ProductRepository>()
                .LifeStyle.Transient);

        }

        void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            //Intercept all methods of all repositories.
            if (UnitOfWorkHelper.IsRepositoryClass(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }

            //Intercept all methods of classes those have at least one method that has UnitOfWork attribute.
            foreach (var method in handler.ComponentModel.Implementation.GetMethods())
            {
                if (UnitOfWorkHelper.HasUnitOfWorkAttribute(method))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
                    return;
                }
            }
        }
    }

    public static class OrmSessionFactory
    {

        static string db_file = HostingEnvironment.MapPath("~/App_Data/agile.db");
        public static NHibernate.ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                SQLiteConfiguration.Standard
                .UsingFile(db_file)
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductMap>())
                //.ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        public static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            // delete the existing db on each run
            if (File.Exists(db_file))
                File.Delete(db_file);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config).Create(false, true);
        }
    }
}