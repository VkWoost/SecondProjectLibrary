using Library.DAL.EF;
using Library.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Library.DAL.Repositories
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private DbSet<TEntity> _dbSet;

        public EFGenericRepository(string conn)
        {
            _context = new BookContext(conn);
            _dbSet = _context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }
        
        public virtual void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _dbSet.Find(id);
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}
