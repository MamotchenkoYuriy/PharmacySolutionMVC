using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using PharmacySolution.Web.Core.Models;
using FluentValidation.Mvc;

namespace PharmacySolution.Web.Core.Validators
{
    public class OrderDetailsCreateViewModelValidator : AbstractValidator<OrderDetailsCreateViewModel>
    {
        public OrderDetailsCreateViewModelValidator()
        {
            RuleFor(m => m.Count).GreaterThan(0).WithMessage("Count cant be less then 1!");
            RuleFor(m => m.UnitPrice).GreaterThan(0).WithMessage("Count cant be less then 1!");
            RuleFor(m => m.OrderId).NotNull().WithMessage("Order must be selected!");
            RuleFor(m => m.MedicamentId).NotNull().WithMessage("Medicamenet mast be selected!");
        }
    }
}
