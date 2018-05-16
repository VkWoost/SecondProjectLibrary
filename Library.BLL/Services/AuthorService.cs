using AutoMapper;
using Library.BLL.Infrastructure;
using Library.DAL.Repositories;
using Library.Entities.Entities;
using System.Collections.Generic;
using Library.ViewModels.ViewModels;

namespace Library.BLL.Services
{
    public class AuthorService
    {
        private EFGenericRepository<Author> _authorRepository;

        public AuthorService(string connectionString)
        {
            _authorRepository = new EFGenericRepository<Author>(connectionString);
        }

        public void AddAuthor(AuthorViewModel authorViewModel)
        {
            _authorRepository.Create(Mapper.Map<AuthorViewModel, Author>(authorViewModel));
        }

        public void DeleteAuthor(int id)
        {
            _authorRepository.Delete(id);
        }

        public IEnumerable<AuthorViewModel> GetAuthors()
        {
            return Mapper.Map<IEnumerable<Author>, List<AuthorViewModel>>(_authorRepository.GetAll());
        }

        public AuthorViewModel GetAuthor(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Id author not found", "");
            }
            var author = _authorRepository.Get(id.Value);
            if (author == null)
            {
                throw new ValidationException("Author not found", "");
            }
            return Mapper.Map<Author, AuthorViewModel>(author);
        }

        public void UpdateAuthor(AuthorViewModel authorViewModel)
        {
            _authorRepository.Update(Mapper.Map<AuthorViewModel, Author>(authorViewModel));
        }
    }
}
