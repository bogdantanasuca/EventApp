using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EventApp.Data.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbset;

        public Repository(EventAppDataContext context)
        {
            this.context = context;      
            dbset = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }
    }
}
