using AutoMapper;
using Library.Entities.Entities;
using Library.ViewModels.ViewModels;

namespace Library.BLL.MappingProiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookViewModel>();
            CreateMap<BookViewModel, Book>();
        }
    }
}
