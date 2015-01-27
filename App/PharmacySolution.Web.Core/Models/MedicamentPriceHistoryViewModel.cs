using System;
using FluentValidation.Attributes;
using PharmacySolution.Core;

namespace PharmacySolution.Web.Core.Models
{
    //[Validator(typeof())]
    public class MedicamentPriceHistoryViewModel: BaseEntity
    {
        public string MedicamentName { get; set; }
        public int MedicamentId { get; set; }
        public decimal Price { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
