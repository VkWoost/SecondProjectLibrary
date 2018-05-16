using AutoMapper;
using Library.BLL.Infrastructure;
using Library.DAL.Interfaces;
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
            Magazine magazine = new Magazine { Name = magazineViewModel.Name, Number = magazineViewModel.Number, YearOfPublication = magazineViewModel.YearOfPublication };
            _magazineRepository.Create(magazine);
        }

        public void DeleteMagazine(int id)
        {
            _magazineRepository.Delete(id);
        }

        public IEnumerable<MagazineViewModel> GetMagazines()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Magazine, MagazineViewModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Magazine>, List<MagazineViewModel>>(_magazineRepository.GetAll());
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
            return new MagazineViewModel { Id = magazine.Id, Name = magazine.Name, Number = magazine.Number, YearOfPublication = magazine.YearOfPublication };
        }

        public void UpdateMagazine(MagazineViewModel magazineViewModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MagazineViewModel, Magazine>()).CreateMapper();
            var magazine = mapper.Map<MagazineViewModel, Magazine>(magazineViewModel);
            _magazineRepository.Update(magazine);
        }
    }
}
