using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PharmacySolution.Core;

namespace PharmacySolution.Data.Mapping
{
    public class OrderMap :EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.OperationDate).IsRequired().HasColumnName("OperationDate");
            Property(m => m.OperationType).IsRequired().HasColumnName("OperationType");
            Property(m => m.PharmacyId).IsRequired().HasColumnName("PharmacyId");
            HasRequired(m=>m.Pharmacy).WithMany(m=>m.Orders).HasForeignKey(m=>m.PharmacyId).WillCascadeOnDelete(true);

        }
    }
}
