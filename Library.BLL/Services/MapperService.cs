using AutoMapper;
using Library.Entities.Entities;
using Library.ViewModels.ViewModels;

namespace Library.BLL.Services
{
    public static class MapperService
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Magazine, MagazineViewModel>();
                cfg.CreateMap<MagazineViewModel, Magazine>();
            });

            return config;
        }
    }
}
