using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Entities.Attributes
{
    public static class UnitOfWorkHelper
    {
        public static bool IsRepositoryMethod(MethodInfo methodInfo)
        {
            return IsRepositoryClass(methodInfo.DeclaringType);
        }

        public static bool IsRepositoryClass(Type type)
        {
            return typeof(IRepository).IsAssignableFrom(type);
        }

        public static bool HasUnitOfWorkAttribute(MethodInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }
    }
}
