using AutoMapper;
using Library.BLL.Infrastructure;
using Library.DAL.Repositories;
using Library.Enteties.Entities;
using System.Collections.Generic;
using ViewModels.ViewModels;

namespace Library.BLL.Services
{
    public class BookService
    {
        private BookRepository _bookRepository;
        private EFGenericRepository<PublicationHouse> _publicationRepository;
        private EFGenericRepository<Author> _authorRepository;

        public BookService(string connectionString)
        {
            _bookRepository = new BookRepository(connectionString);
            _publicationRepository = new EFGenericRepository<PublicationHouse>(connectionString);
            _authorRepository = new EFGenericRepository<Author>(connectionString);
        }

        public void AddBook(BookViewModel bookViewModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookViewModel, Book>()).CreateMapper();
            var book = mapper.Map<BookViewModel, Book>(bookViewModel);
            _bookRepository.Create(book);
        }

        public void DeleteBook(int id)
        {
            _bookRepository.Delete(id);
        }
        
        public BookViewModel GetBook(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Id book not found", "");
            }
            var book = _bookRepository.Get(id.Value);
            if (book == null)
            {
                throw new ValidationException("Book not found", "");
            }

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PublicationHouse, PublicationHouseViewModel>()).CreateMapper();
            List<PublicationHouseViewModel> pH = new List<PublicationHouseViewModel>();
            foreach (var i in book.PublicationHouses)
            {
                pH.Add(mapper.Map< PublicationHouse, PublicationHouseViewModel>( _publicationRepository.Get(i.Id)));
            }
            return new BookViewModel
            {
                Id = book.Id,
                AuthorId = book.AuthorId,
                Name = book.Name,
                YearOfPublication = book.YearOfPublication,
                PublicationHouses = pH
            };
        }

        public IEnumerable<BookViewModel> GetBooks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookViewModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Book>, List<BookViewModel>>(_bookRepository.GetAll());
        }

        public void UpdateBook(BookViewModel bookViewModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookViewModel, Book>()).CreateMapper();
            var book = mapper.Map<BookViewModel, Book>(bookViewModel);
            _bookRepository.Update(book);
        }      
    }
}
