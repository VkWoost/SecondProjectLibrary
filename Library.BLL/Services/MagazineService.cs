using AutoMapper;
using Library.BLL.Infrastructure;
using Library.DAL.Repositories;
using Library.Entities.Entities;
using System.Collections.Generic;
using Library.ViewModels.ViewModels;

namespace Library.BLL.Services
{
    public class MagazineService
    {
        private EFGenericRepository<Magazine> _magazineRepository;

        public MagazineService(string connectionString)
        {
            _magazineRepository = new EFGenericRepository<Magazine>(connectionString);
        }

        public void AddMagazine(MagazineViewModel magazineViewModel)
        {
            _magazineRepository.Create(Mapper.Map<MagazineViewModel, Magazine>(magazineViewModel));
        }

        public void DeleteMagazine(int id)
        {
            _magazineRepository.Delete(id);
        }

        public IEnumerable<MagazineViewModel> GetMagazines()
        {
            return Mapper.Map<IEnumerable<Magazine>, List<MagazineViewModel>>(_magazineRepository.GetAll());
        }

        public MagazineViewModel GetMagazine(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Id magazine not found", "");
            }
            var magazine = _magazineRepository.Get(id.Value);
            if (magazine == null)
            {
                throw new ValidationException("Magazine not found", "");
            }
            return Mapper.Map<Magazine, MagazineViewModel>(magazine);
        }

        public void UpdateMagazine(MagazineViewModel magazineViewModel)
        {
            _magazineRepository.Update(Mapper.Map<MagazineViewModel,Magazine>(magazineViewModel));
        }
    }
}
