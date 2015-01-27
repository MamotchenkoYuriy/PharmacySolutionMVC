using System.Linq;
using PharmacySolution.Contracts.Repository;
using PharmacySolution.Contracts.Validator;
using PharmacySolution.Core;

namespace PharmacySolution.BusinessLogic.Validators
{
    public class MedicamentPriceHistoryValidator : IValidator<MedicamentPriceHistory>
    {
        private readonly IRepository<MedicamentPriceHistory> _medicamentPriceHistoryRepository;

        public MedicamentPriceHistoryValidator(IRepository<MedicamentPriceHistory> medicamentPriceHistoryRepository)
        {
            _medicamentPriceHistoryRepository = medicamentPriceHistoryRepository;
        }

        public bool IsValid(MedicamentPriceHistory entity)
        {
            var medicamentPriceHistory = _medicamentPriceHistoryRepository.FindAll().Where(m => m.MedicamentId == entity.MedicamentId).Any(history => history.MedicamentId == entity.MedicamentId && history.Price == entity.Price);
            return !medicamentPriceHistory;
        }
    }
}
