using AutoMapper;
using Library.BLL.Infrastructure;
using Library.DAL.Interfaces;
using Library.DAL.Repositories;
using Library.Enteties.Entities;
using System.Collections.Generic;
using ViewModels.ViewModels;

namespace Library.BLL.Services
{
    public class BrochureService
    {
        private EFGenericRepository<Brochure> _brochureRepository;

        public BrochureService(string connectionString)
        {
            _brochureRepository = new EFGenericRepository<Brochure>(connectionString);
        }

        public void AddBrochure(BrochureViewModel brochureViewModel)
        {
            Brochure brochure = new Brochure
            {
                Name = brochureViewModel.Name,
                NumberOfPages = brochureViewModel.NumberOfPages,
                TypeOfCover = brochureViewModel.TypeOfCover
            };
            _brochureRepository.Create(brochure);
        }

        public void DeleteBrochure(int id)
        {
            _brochureRepository.Delete(id);
        }

        public BrochureViewModel GetBrochure(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Id brochure not found", "");
            }
            var brochure = _brochureRepository.Get(id.Value);
            if (brochure == null)
            {
                throw new ValidationException("Brochure not found", "");
            }
            return new BrochureViewModel { Id = brochure.Id, Name = brochure.Name, NumberOfPages = brochure.NumberOfPages, TypeOfCover = brochure.TypeOfCover };
        }

        public IEnumerable<BrochureViewModel> GetBrochures()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Brochure, BrochureViewModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Brochure>, List<BrochureViewModel>>(_brochureRepository.GetAll());
        }

        public void UpdateBrochure(BrochureViewModel brochureViewModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BrochureViewModel, Brochure>()).CreateMapper();
            var brochure = mapper.Map<BrochureViewModel, Brochure>(brochureViewModel);
            _brochureRepository.Update(brochure);
        }
    }
}
