using AutoMapper;
using Library.BLL.Infrastructure;
using Library.DAL.Repositories;
using Library.Entities.Entities;
using System.Collections.Generic;
using Library.ViewModels.ViewModels;

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
            _publicationServiceRepository.Create(Mapper.Map<PublicationHouseViewModel, PublicationHouse>(publicationHouseViewModel));
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
            return Mapper.Map<PublicationHouse, PublicationHouseViewModel>(publicationHouse);
        }

        public IEnumerable<PublicationHouseViewModel> GetPublicationHouses()
        {
            return Mapper.Map<IEnumerable<PublicationHouse>, List<PublicationHouseViewModel>>(_publicationServiceRepository.GetAll());
        }

        public void UpdatePublicationHouse(PublicationHouseViewModel publicationHouseViewModel)
        {
            _publicationServiceRepository.Update(Mapper.Map<PublicationHouseViewModel, PublicationHouse>(publicationHouseViewModel));
        }
    }
}
