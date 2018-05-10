using Library.DAL.EF;
using Library.Enteties.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Library.DAL.Repositories
{
    public class BookRepository
    {
        DbContext _context;
        DbSet<Book> _dbSet;
        DbSet<Author> _authorDbSet;
        DbSet<PublicationHouse> _pHDbSet;

        public BookRepository(string conn)
        {
            _context = new BookContext(conn);
            _dbSet = _context.Set<Book>();
            _authorDbSet = _context.Set<Author>();
            _pHDbSet = _context.Set<PublicationHouse>();
        }

        public Book Get(int id)
        {
            return _dbSet.Include(x => x.PublicationHouses).Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Book> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }
        
        public void Create(Book item)
        {
            var book = new Book
            {
                Name = item.Name,
                AuthorId = item.AuthorId,
                YearOfPublication = item.YearOfPublication
            };
            book.Author = _authorDbSet.Where(x => x.Id == item.AuthorId).FirstOrDefault();            
            foreach(var i in item.PublicationHouses)
            {
                book.PublicationHouses.Add(_pHDbSet.Where(x => x.Id == i.Id).FirstOrDefault());
            }
            book.Author.Books.Add(book);
            _dbSet.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book item)
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

        public void Delete(int id)
        {
            var item = _dbSet.Find(id);
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}
