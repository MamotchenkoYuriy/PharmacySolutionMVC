using System.Data.Entity.ModelConfiguration;
using PharmacySolution.Core;

namespace PharmacySolution.Data.Mapping
{
    public class StorageMap :EntityTypeConfiguration<Storage>
    {
        public StorageMap()
        {
            HasKey(m => new {m.MedicamentId, m.PharmacyId});
            Property(m => m.Count).IsRequired().HasColumnName("Count");

            HasRequired(m => m.Medicament).WithMany(m => m.Storages).HasForeignKey(storage => storage.MedicamentId);
            HasRequired(m => m.Pharmacy).WithMany(m => m.Storages).HasForeignKey(storage => storage.PharmacyId);
        }
    }
}
