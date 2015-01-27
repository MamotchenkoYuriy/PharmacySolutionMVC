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
    public class PharmacyViewValidator : AbstractValidator<PharmacyViewModel>
    {
        public PharmacyViewValidator()
        {
            RuleFor(m => m.Address).NotNull().Length(5, 150).WithMessage("To short of to large address!");
            RuleFor(m => m.Number).Matches(new Regex(@"^\d{3}-\d{3}-\d{2}-\d{2}$")).WithMessage("Number mast be like [***-***-**-**]!");
            RuleFor(m => m.OpenDate).NotNull().WithMessage("Date cant be empty!");
            RuleFor(m => m.PhoneNumber).NotNull().WithMessage("Not a phone number!");
        }
    }
}
