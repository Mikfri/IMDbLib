using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.Repository
{
    public interface IGRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T id);
        IEnumerable<T> RunStoredProcedure(string procedureName, params object[] parameters);
    }
}
