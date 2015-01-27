using System.Linq;
using PharmacySolution.Contracts.Repository;
using PharmacySolution.Contracts.Validator;
using PharmacySolution.Core;

namespace PharmacySolution.BusinessLogic.Validators
{
    public class PharmacyValidator: IValidator<Pharmacy>
    {
        private readonly IRepository<Pharmacy> _pharmacyRepository;
        public PharmacyValidator(IRepository<Pharmacy> pharmacyRepository)
        {
            _pharmacyRepository = pharmacyRepository;
        }
        public bool IsValid(Pharmacy entity)
        {
            return
                _pharmacyRepository.FindAll().FirstOrDefault(m => m.Number == entity.Number 
                    || m.PhoneNumber == entity.PhoneNumber ||
                    m.Address == entity.Address) == null;
        }
    }
}
