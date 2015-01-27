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
    public class MedicamentViewValidator: AbstractValidator<MedicamentViewModel>
    {
        public MedicamentViewValidator()
        {
            RuleFor(m => m.Description).NotNull().Length(10, 250).WithMessage("To short of to small description");
            RuleFor(m => m.Name).NotNull().WithMessage("Medicament name cant be null");
            RuleFor(m => m.Price).GreaterThan(0).WithMessage("Medicament price cant be null");
            RuleFor(m => m.SerialNumber).Matches(new Regex(@"^\d{3}-\d{3}-\d{2}-\d{2}$")).WithMessage("Number mast be like [***-***-**-**]!");
        }
    }
}
