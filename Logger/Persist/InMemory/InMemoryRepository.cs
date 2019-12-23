using System;
using System.Linq;
using System.Runtime.Caching;
using Logger.Models;

namespace Logger.Persist.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T: BaseEntity
    {
        // May swap out default cache
        private ObjectCache _cache = MemoryCache.Default;

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Insert(T t)
        {
            throw new NotImplementedException();
        }

        public void Update(T t)
        {
            throw new NotImplementedException();
        }

        public T Find(string Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Collection()
        {
            throw new NotImplementedException();
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
