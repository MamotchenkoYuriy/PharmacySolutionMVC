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
    [Validator(typeof(OrderViewModelValidator))]
    public class OrderViewModel : BaseEntity
    {
        public int PharmacyId { get; set; }
        public string PharmacyNumber { get; set; }
        public DateTime OperationDate { get; set; }
        public OperationType OperationType { get; set; }
    }
}
