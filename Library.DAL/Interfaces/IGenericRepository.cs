using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        //IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Delete(int id);
        void Update(TEntity item);
    }
}
