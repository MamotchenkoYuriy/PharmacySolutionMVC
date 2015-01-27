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
    [Validator(typeof(PharmacyViewValidator))]
    public class PharmacyViewModel : BaseEntity
    {
        public string Address { get; set; }
        public string Number { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime OpenDate { get; set; }
    }
}
