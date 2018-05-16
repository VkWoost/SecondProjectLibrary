using AutoMapper;
using Library.BLL.Infrastructure;
using Library.DAL.Repositories;
using Library.Entities.Entities;
using System.Collections.Generic;
using Library.ViewModels.ViewModels;

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
            _brochureRepository.Create(Mapper.Map<BrochureViewModel, Brochure>(brochureViewModel));
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
            return Mapper.Map<Brochure, BrochureViewModel>(brochure);
        }

        public IEnumerable<BrochureViewModel> GetBrochures()
        {
            return Mapper.Map<IEnumerable<Brochure>, List<BrochureViewModel>>(_brochureRepository.GetAll());
        }

        public void UpdateBrochure(BrochureViewModel brochureViewModel)
        {
            _brochureRepository.Update(Mapper.Map<BrochureViewModel, Brochure>(brochureViewModel));
        }
    }
}
