using AutoMapper;
using Library.Entities.Entities;
using Library.ViewModels.ViewModels;

namespace Library.BLL.MappingProiles
{
    public class PublicationHouseProfile : Profile
    {
        public PublicationHouseProfile()
        {
            CreateMap<PublicationHouse, PublicationHouseViewModel>();
            CreateMap<PublicationHouseViewModel, PublicationHouse>();
        }
    }
}
