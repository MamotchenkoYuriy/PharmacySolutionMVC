using System.Linq;
using PharmacySolution.Contracts.Repository;
using PharmacySolution.Contracts.Validator;
using PharmacySolution.Core;

namespace PharmacySolution.BusinessLogic.Validators
{
    public class MedicamentValidator : IValidator<Medicament>
    {
        private readonly IRepository<Medicament> _medicamentRepository;

        public MedicamentValidator(IRepository<Medicament> medicamentRepository)
        {
            _medicamentRepository = medicamentRepository; 
        }

        public bool IsValid(Medicament entity)
        {
            return _medicamentRepository.GetByPrimaryKey(entity.Id) == null || _medicamentRepository.Find(medicament => medicament.Name == entity.Name).FirstOrDefault() == null;
        }
    }
}
