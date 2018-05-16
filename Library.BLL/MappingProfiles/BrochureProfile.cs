using AutoMapper;
using Library.Entities.Entities;
using Library.ViewModels.ViewModels;

namespace Library.BLL.MappingProiles
{
    public class BrochureProfile : Profile
    {
        public BrochureProfile()
        {
            CreateMap<Brochure, BrochureViewModel>();
            CreateMap<BrochureViewModel, Brochure>();
        }
    }
}
