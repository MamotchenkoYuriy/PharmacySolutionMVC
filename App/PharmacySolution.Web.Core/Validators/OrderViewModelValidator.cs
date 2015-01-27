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
    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {
            RuleFor(m => m.PharmacyNumber).Matches(new Regex(@"^\d{3}-\d{3}-\d{2}-\d{2}$")).WithMessage("Number mast be like [***-***-**-**]!");
            RuleFor(m => m.OperationDate).NotNull().WithMessage("Date cant be empty!");
            RuleFor(m => m.OperationType).NotNull().WithMessage("Orror operation type!");
        }
    }
}
