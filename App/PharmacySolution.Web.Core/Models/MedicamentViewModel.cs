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
    [Validator(typeof(MedicamentViewValidator))]
    public class MedicamentViewModel : BaseEntity
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
