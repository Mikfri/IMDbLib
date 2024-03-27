using IMDbLib.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.Repository
{
    public class GRepository<T> : IGRepository<T> where T : class
    {
        private readonly IMDb_Context _context;
        private readonly DbSet<T> _dbSet;

        public GRepository(IMDb_Context context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        //public IEnumerable<T> GetAll()
        //{
        //    return _dbSet.ToList();
        //}

        public T Get(string id)
        {
            return _dbSet.Find(id);
        }
        
        public IEnumerable<T> RunStoredProcedure(string procedureName, params object[] parameters)
        {
            var parameterString = string.Join(", ", parameters.Select((p, i) => $"@p{i}"));
            return _dbSet.FromSqlRaw($"EXEC {procedureName} {parameterString}", parameters).ToList();
        }

        // EF includes bruges her for at hente data fra flere tabeller
        //public T GetWithIncludes<TKey>(TKey id, Expression<Func<T, bool>> idExpression, params Expression<Func<T, object>>[] includeProperties)
        //{
        //    IQueryable<T> query = _dbSet;
        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);
        //    }
        //    return query.SingleOrDefault(idExpression);
        //}

        //public T GetWithIncludes(string id, params Expression<Func<T, object>>[] includeProperties)
        //{
        //    IQueryable<T> query = _dbSet;
        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);
        //    }
        //    return query.SingleOrDefault(e => e.Id == id);
        //}


    }
}
