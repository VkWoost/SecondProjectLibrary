using AutoMapper;
using Library.BLL.Infrastructure;
using Library.DAL.Repositories;
using Library.Enteties.Entities;
using System.Collections.Generic;
using ViewModels.ViewModels;

namespace Library.BLL.Services
{
    public class PublicationHouseService
    {
        private EFGenericRepository<PublicationHouse> _publicationServiceRepository;

        public PublicationHouseService(string connectionString)
        {
            _publicationServiceRepository = new EFGenericRepository<PublicationHouse>(connectionString);
        }

        public void AddPublicationHouse(PublicationHouseViewModel publicationHouseViewModel)
        {
            PublicationHouse publicationHouse = new PublicationHouse
            {
                Name = publicationHouseViewModel.Name,
                Adress = publicationHouseViewModel.Adress
            };
            _publicationServiceRepository.Create(publicationHouse);
        }

        public void DeletePublicationHouse(int id)
        {
            _publicationServiceRepository.Delete(id);
        }

        public PublicationHouseViewModel GetPublicationHouse(int? id)
        {
            PublicationHouse publicationHouse = _publicationServiceRepository.Get(id.Value);
            if (publicationHouse == null)
            {
                throw new ValidationException("Publication House not found", "");
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PublicationHouse, PublicationHouseViewModel>()).CreateMapper();
            return mapper.Map<PublicationHouse, PublicationHouseViewModel>(publicationHouse);
        }

        public IEnumerable<PublicationHouseViewModel> GetPublicationHouses()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PublicationHouse, PublicationHouseViewModel>()).CreateMapper();
            return mapper.Map<IEnumerable<PublicationHouse>, List<PublicationHouseViewModel>>(_publicationServiceRepository.GetAll());
        }

        public void UpdatePublicationHouse(PublicationHouseViewModel publicationHouseViewModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PublicationHouseViewModel, PublicationHouse>()).CreateMapper();
            var publicationHouse = mapper.Map<PublicationHouseViewModel, PublicationHouse>(publicationHouseViewModel);
            _publicationServiceRepository.Update(publicationHouse);
        }
    }
}
