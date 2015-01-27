using PharmacySolution.Contracts;

namespace PharmacySolution.Core
{
    public class Storage :IDbEntity
    {
        public int MedicamentId { get; set; }
        public int PharmacyId { get; set; }
        public int Count { get; set; }
        public virtual Pharmacy Pharmacy { get; set; }
        public virtual Medicament Medicament { get; set; }

        public Storage()
        {
            
        }
    }
}