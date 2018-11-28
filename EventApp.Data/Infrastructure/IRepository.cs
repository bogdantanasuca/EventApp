using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EventApp.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        IEnumerable<T> Get(Expression<Func<T, bool>> where);

        IQueryable<T> Query();
    }
}