using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Repositories
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Insert(T obj);
        T Update(T obj);
        void Delete(T obj);
        int Save();
    }
}
