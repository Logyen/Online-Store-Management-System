using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    internal interface IGenericRepository<T>
    {

        void Add(T entity);
        void Remove(T entity);
        T GetById(int id);
        List<T> GetAll();
    }
}
