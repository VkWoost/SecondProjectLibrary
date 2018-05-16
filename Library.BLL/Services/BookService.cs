using AutoMapper;
using Library.BLL.Infrastructure;
using Library.DAL.Repositories;
using Library.Entities.Entities;
using System.Collections.Generic;
using Library.ViewModels.ViewModels;
using System.Linq;

namespace Library.BLL.Services
{
    public class BookService
    {
        private BookRepository _bookRepository;
        private AuthorService _authorService;

        public BookService(string connectionString)
        {
            _bookRepository = new BookRepository(connectionString);
            _authorService = new AuthorService(connectionString);
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
        
        //public BookViewModel GetBook(int? id)
        //{
        //    if (id == null)
        //    {
        //        throw new ValidationException("Id book not found", "");
        //    }
        //    var book = _bookRepository.Get(id.Value);
        //    if (book == null)
        //    {
        //        throw new ValidationException("Book not found", "");
        //    }

        //    var mapperBook = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<PublicationHouse, PublicationHouseViewModel>();
        //        cfg.CreateMap<Book, BookViewModel>();
        //    }).CreateMapper();
        //    var result = mapperBook.Map<Book, BookViewModel>(book);
        //    result.Author = _authorService.GetAuthor(result.AuthorId);
        //    return result;
        //}

        public IEnumerable<BookViewModel> GetBooks()
        {
            List<Book> books = new List<Book>(_bookRepository.GetAll());
            var mapperBook = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PublicationHouse, PublicationHouseViewModel>();
                cfg.CreateMap<Book, BookViewModel>();
            }).CreateMapper();
            List <BookViewModel> result = new List<BookViewModel>();
            foreach (var book in books)
            {
                result.Add(mapperBook.Map<Book, BookViewModel>(book));
            }
            var authors = _authorService.GetAuthors();
            foreach (var book in result)
            {
                book.Author = authors.Where(x => x.Id == book.AuthorId).FirstOrDefault();
            }
            return result;
        }

        public void UpdateBook(BookViewModel bookViewModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookViewModel, Book>()).CreateMapper();
            var book = mapper.Map<BookViewModel, Book>(bookViewModel);
            _bookRepository.Update(book);
        }      
    }
}
