using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using PharmacySolution.Web.Core.Models;
using FluentValidation.Mvc;
using PharmacySolution.Core;

namespace PharmacySolution.Web.Core.Validators
{
    public class StorageCreateViewModelValidator : AbstractValidator<StorageCreateViewModel>
    {
        public StorageCreateViewModelValidator()
        {
            RuleFor(m => m.PharmacyId).GreaterThan(0).WithMessage("PharmacyId must be not null!");
            RuleFor(m => m.MedicamentId).GreaterThan(0).WithMessage("MedicamentId must be not null!");
            RuleFor(m => m.Count).GreaterThan(0).WithMessage("Count must be more then 0!");
        }
    }
}
