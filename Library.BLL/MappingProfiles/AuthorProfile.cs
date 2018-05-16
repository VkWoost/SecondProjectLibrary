using AutoMapper;
using Library.Entities.Entities;
using Library.ViewModels.ViewModels;

namespace Library.BLL.MappingProiles
{
    public class AuthorProfile : Profile 
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorViewModel>();
            CreateMap<AuthorViewModel, Author>();
        }
    }
}
