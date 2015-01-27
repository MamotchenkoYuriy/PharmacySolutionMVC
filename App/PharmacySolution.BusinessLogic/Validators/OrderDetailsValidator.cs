using System.Linq;
using PharmacySolution.Contracts.Repository;
using PharmacySolution.Contracts.Validator;
using PharmacySolution.Core;

namespace PharmacySolution.BusinessLogic.Validators
{
    public class OrderDetailsValidator: IValidator<OrderDetails>
    {
        private readonly IRepository<OrderDetails> _orderDetailsRepository = null;
        public OrderDetailsValidator(IRepository<OrderDetails> orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }
        
        public bool IsValid(OrderDetails entity)
        {
            return
                _orderDetailsRepository.FindAll().FirstOrDefault(m => m.MedicamentId == entity.MedicamentId && m.OrderId == entity.OrderId) == null;
        }
    }
}
