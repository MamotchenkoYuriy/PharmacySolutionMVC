using System.Collections.Generic;

namespace PharmacySolution.Core
{
    public class Medicament : BaseEntity
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        //public string Manufactorer { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<MedicamentPriceHistory> MedicamentPriceHistories { get; set; }
        public virtual ICollection<OrderDetails> OrderDetailses { get; set; } 
        public virtual ICollection<Storage> Storages { get; set; }

        public Medicament()
        {
            MedicamentPriceHistories = new List<MedicamentPriceHistory>();
            OrderDetailses = new List<OrderDetails>();
            Storages = new List<Storage>();
        }
    }
}
