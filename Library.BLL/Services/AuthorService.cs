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
            Author author = new Author
            {
                Name = authorViewModel.Name,
            };
            _authorRepository.Create(author);
        }

        public void DeleteAuthor(int id)
        {
            _authorRepository.Delete(id);
        }

        public IEnumerable<AuthorViewModel> GetAuthors()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Author, AuthorViewModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Author>, List<AuthorViewModel>>(_authorRepository.GetAll());
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
            return new AuthorViewModel { Id = author.Id, Name = author.Name };
        }

        public void UpdateAuthor(AuthorViewModel authorViewModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AuthorViewModel, Author>()).CreateMapper();
            var author = mapper.Map<AuthorViewModel, Author>(authorViewModel);
            _authorRepository.Update(author);
        }
    }
}
