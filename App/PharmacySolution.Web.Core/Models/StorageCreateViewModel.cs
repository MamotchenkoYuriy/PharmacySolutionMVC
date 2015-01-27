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
    [Validator(typeof(StorageCreateViewModelValidator))]
    public class StorageCreateViewModel : BaseEntity
    {
        public int MedicamentId { get; set; }
        public int PharmacyId { get; set; }
        public int Count { get; set; }
    }
}
