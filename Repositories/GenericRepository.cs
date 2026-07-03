using System.Collections.Generic;
using OnlineStore.Models;

namespace OnlineStore.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private List<T> items;

        public GenericRepository()
        {
            items = new List<T>();
        }

        public void Add(T entity)
        {
            items.Add(entity);
        }

        public void Remove(T entity)
        {
            items.Remove(entity);
        }

        public T GetById(int id)
        {
            foreach (T item in items)
            {
                if (item.Id == id)
                    return item;
            }

            return null;
        }

        public List<T> GetAll()
        {
            return items;
        }
    }
}