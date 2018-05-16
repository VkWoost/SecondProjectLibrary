using AutoMapper;
using Library.Entities.Entities;
using Library.ViewModels.ViewModels;

namespace Library.BLL.MappingProiles
{
    public class MagazineProfile : Profile
    {
        public MagazineProfile()
        {
            CreateMap<Magazine, MagazineViewModel>();
            CreateMap<MagazineViewModel, Magazine>();
        }
    }
}
