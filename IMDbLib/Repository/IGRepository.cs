using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.Repository
{
    public interface IGRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        IEnumerable<T> RunStoredProcedure(string procedureName, params object[] parameters);
        //T GetWithIncludes<TKey>(TKey id, Expression<Func<T, bool>> idExpression, params Expression<Func<T, object>>[] includeProperties);
        //T GetWithIncludes(string id, params Expression<Func<T, object>>[] includeProperties);
    }
}
