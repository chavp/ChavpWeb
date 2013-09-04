using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Entities
{
    public interface IRepository
    {

    }

    public interface IRepository<TEntity, TID>
        : IRepository where TEntity : class
    {
        /// <summary>
        /// Gets all objects from database
        /// </summary>
        IQueryable<TEntity> All();

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        IQueryable<TEntity> Filter<Key>(Expression<Func<TEntity, bool>> filter,
            out int total, int index = 0, int size = 50);

        /// <summary>
        /// Get the total objects count.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        TEntity Find(TID id);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate"></param>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        object Save(TEntity model);

        void Delete(TEntity model);

        void SaveOrUpdate(TEntity model);
    }
}
