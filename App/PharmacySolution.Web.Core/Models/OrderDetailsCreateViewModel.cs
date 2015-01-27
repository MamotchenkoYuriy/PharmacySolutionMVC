using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Attributes;
using PharmacySolution.Core;
using PharmacySolution.Web.Core.Validators;

namespace PharmacySolution.Web.Core.Models
{
    [Validator(typeof(OrderDetailsCreateViewModelValidator))]
    public class OrderDetailsCreateViewModel:BaseEntity
    {
        public int OrderId { get; set; }
        public int MedicamentId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Count { get; set; }
    }
}
