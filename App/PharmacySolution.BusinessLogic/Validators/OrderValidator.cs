using System.Linq;
using PharmacySolution.Contracts.Repository;
using PharmacySolution.Contracts.Validator;
using PharmacySolution.Core;

namespace PharmacySolution.BusinessLogic.Validators
{
    public class OrderValidator:IValidator<Order>
    {
        private readonly IRepository<Order> _orderRepository = null;
        public OrderValidator(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public bool IsValid(Order entity)
        {
            return _orderRepository.Find(m => m.Id == entity.Id).FirstOrDefault() == null;
        }
    }
}
