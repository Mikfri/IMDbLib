using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.Repository
{
    public class GRepository<T> : IGRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Get(string id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }        

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
         
        public IEnumerable<T> RunStoredProcedure(string procedureName, params object[] parameters)
        {
            var parameterString = string.Join(", ", parameters.Select((p, i) => $"@p{i}"));
            return _dbSet.FromSqlRaw($"EXEC {procedureName} {parameterString}", parameters).ToList();
        }
    }
}
