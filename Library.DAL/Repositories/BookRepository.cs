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
        private DbSet<PublicationHouse> _pHSet;

        public BookRepository(string conn)
            : base(conn)
        {
            _context = new BookContext(conn);
            _dbSet = _context.Set<Book>();
            _pHSet = _context.Set<PublicationHouse>();
            _authorDbSet = _context.Set<Author>();
        }
        
        public override void Create(Book item)
        {
            item.Author = _authorDbSet.Find(item.AuthorId);
            List<int> pHIds = new List<int>(item.PublicationHouses.Select(x => x.Id));
            List<PublicationHouse> pH = new List<PublicationHouse>(_pHSet.Where(x => pHIds.Contains(x.Id)).ToList());
            item.PublicationHouses = new List<PublicationHouse>(pH);

            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public override void Update(Book item)
        {
            Book book = _dbSet.Include(x => x.PublicationHouses).Where(z => z.Id == item.Id).FirstOrDefault();
            book.Name = item.Name;
            book.AuthorId = item.AuthorId;
            book.Author = _authorDbSet.Find(item.AuthorId);
            book.YearOfPublication = item.YearOfPublication;
            List<int> pHIds = new List<int>(item.PublicationHouses.Select(x => x.Id));
            List<PublicationHouse> pH = new List<PublicationHouse>(_pHSet.Where(x => pHIds.Contains(x.Id)).ToList());
            book.PublicationHouses = new List<PublicationHouse>(pH);

            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public override IEnumerable<Book> GetAll()
        {
            return _dbSet.Include(x => x.PublicationHouses).AsNoTracking().ToList();
        }
    }
}
