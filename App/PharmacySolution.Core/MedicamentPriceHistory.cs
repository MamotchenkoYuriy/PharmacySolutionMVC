using System;

namespace PharmacySolution.Core
{
    public class MedicamentPriceHistory : BaseEntity
    {
        public int MedicamentId { get; set; }
        public decimal Price { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual Medicament Medicament { get; set; }

        public MedicamentPriceHistory()
        {
            
        }
    }
}
