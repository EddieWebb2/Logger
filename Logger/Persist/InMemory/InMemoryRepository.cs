using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Logger.Models;

namespace Logger.Persist.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T: BaseEntity
    {
        // May swap out default cache
        private ObjectCache _cache = MemoryCache.Default;
        private List<T> _items;
        private string _className;


        public InMemoryRepository()
        {
            _className = typeof(T).Name;

            _items = _cache[_className] as List<T>;
            if (_items == null) _items = new List<T>();
        }

        public void Commit() => _cache[_className] = _items;

        public void Insert(T t) => _items.Add(t);

        public void Update(T t)
        {
            T tToUpdate = _items.Find(i => i.Id == t.Id);

            if (tToUpdate == null)
                throw new Exception($"{_className} Not found");
            tToUpdate = t;
        }

        public T Find(string Id)
        {
            T t = _items.Find(i => i.Id == Id);

            if (t == null)
                throw new Exception($"{_className} Not found");
            return t;
        }

        public IQueryable<T> Collection() => _items.AsQueryable();

        public void Delete(string Id)
        {
            T tToDelete = _items.Find(i => i.Id == Id);

            if (tToDelete == null)
                throw new Exception($"{_className} Not found");
            _items.Remove(tToDelete);
        }
    }
}
