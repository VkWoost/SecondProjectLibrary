using Library.DAL.EF;
using Library.Entities.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Library.DAL.Repositories
{
    public class BookRepository : EFGenericRepository<Book>
    {
        private DbContext _context;
        private DbSet<Book> _dbSet;
        private DbSet<Author> _authorDbSet;
        private DbSet<PublicationHouse> _pHDbSet;

        public BookRepository(string conn)
            : base(conn)
        {
            _context = new BookContext(conn);
            _dbSet = _context.Set<Book>();
            _authorDbSet = _context.Set<Author>();
            _pHDbSet = _context.Set<PublicationHouse>();
        }
        
        public override void Create(Book item)
        {
            item.Author = _authorDbSet.Find(item.AuthorId);
            
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public override void Update(Book item)
        {
            var book = _dbSet.Include(x => x.PublicationHouses).Where(x => x.Id == item.Id).FirstOrDefault();
            book.Author = _authorDbSet.Where(x => x.Id == item.AuthorId).FirstOrDefault();
            book.Name = item.Name;
            book.YearOfPublication = item.YearOfPublication;
            book.PublicationHouses.Clear();
            foreach (var i in item.PublicationHouses)
            {
                book.PublicationHouses.Add(_pHDbSet.Where(x => x.Id == i.Id).FirstOrDefault());
            }            
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
