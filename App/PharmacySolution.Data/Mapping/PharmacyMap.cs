using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PharmacySolution.Core;

namespace PharmacySolution.Data.Mapping
{
    public class PharmacyMap :EntityTypeConfiguration<Pharmacy>
    {
        public PharmacyMap()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Number).IsRequired().HasColumnName("Number");
            Property(m => m.OpenDate).IsRequired().HasColumnName("OpenDate");
            Property(m => m.PhoneNumber).IsRequired().HasColumnName("PhoneNumber");
            Property(m => m.Address).IsRequired().HasColumnName("Address");
        }
    }
}
