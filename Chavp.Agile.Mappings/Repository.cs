using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Mappings
{
    using NHibernate;
    using NHibernate.Linq;
    using Chavp.Agile.Entities;
    using System.Linq.Expressions;

    public abstract class Repository<T, ID>
        : IRepository<T, ID> where T : class
    {
        protected ISession Session { get { return UnitOfWork.Current.Session; } }

        public IQueryable<T> All()
        {
            return Session.Query<T>();
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return Session.Query<T>().Where(predicate).AsQueryable<T>();
        }

        public IQueryable<T> Filter<Key>(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? Session.Query<T>().Where(filter).AsQueryable() :
                Session.Query<T>().AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) :
                _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public int Count
        {
            get { return Session.Query<T>().Count(); }
        }

        public T Find(ID id)
        {
            return Session.Get<T>(id);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return Session.Query<T>().FirstOrDefault(predicate);
        }

        public object Save(T model)
        {
            var obj = Session.Save(model);
            return obj;
        }

        public void Delete(T model)
        {
            Session.Delete(model);
        }

        public void SaveOrUpdate(T model)
        {
            Session.SaveOrUpdate(model);
        }

    }
}
