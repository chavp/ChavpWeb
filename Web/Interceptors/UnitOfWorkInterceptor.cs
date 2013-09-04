using Castle.DynamicProxy;
using Chavp.Agile.Entities.Attributes;
using Chavp.Agile.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Web.Interceptors;

namespace Web.Interceptors
{
    public class UnitOfWorkInterceptor
        : IInterceptor
    {
        private readonly NHibernate.ISessionFactory _sessionFactory;

        public UnitOfWorkInterceptor(NHibernate.ISessionFactory sessionFactory)
	    {
		    _sessionFactory = sessionFactory;
	    }

        public void Intercept(IInvocation invocation)
        {
            //If there is a running transaction, just run the method
            if (UnitOfWork.Current != null 
                || !RequiresDbConnection(invocation.MethodInvocationTarget))
            {
                invocation.Proceed();
                return;
            }

            try
            {
                UnitOfWork.Current = new UnitOfWork(_sessionFactory);
                UnitOfWork.Current.BeginTransaction();

                try
                {
                    invocation.Proceed();
                    UnitOfWork.Current.Commit();
                }
                catch
                {
                    try
                    {
                        UnitOfWork.Current.Rollback();
                    }
                    catch
                    {

                    }

                    throw;
                }
            }
            finally
            {
                UnitOfWork.Current = null;
            }
        }

        private static bool RequiresDbConnection(MethodInfo methodInfo)
        {
            if (UnitOfWorkHelper.HasUnitOfWorkAttribute(methodInfo))
            {
                return true;
            }

            if (UnitOfWorkHelper.IsRepositoryMethod(methodInfo))
            {
                return true;
            }

            return false;
        }
    }
}