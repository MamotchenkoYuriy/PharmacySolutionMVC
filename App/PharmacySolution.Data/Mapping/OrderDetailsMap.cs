using System.Data.Entity.ModelConfiguration;
using PharmacySolution.Core;

namespace PharmacySolution.Data.Mapping
{
    public class OrderDetailsMap :EntityTypeConfiguration<OrderDetails>
    {
        public OrderDetailsMap()
        {
            HasKey(m => new {m.MedicamentId, m.OrderId});
            Property(m => m.UnitPrice).IsRequired().HasColumnName("UnitPrice");
            Property(m => m.Count).IsRequired().HasColumnName("Count");
            Property(m => m.OrderId).IsRequired().HasColumnName("OrderId");
            Property(m => m.MedicamentId).IsRequired().HasColumnName("MedicamentId");

            HasRequired(m=>m.Order).WithMany(m=>m.OrderDetailses).HasForeignKey(m=>m.OrderId).WillCascadeOnDelete(true);
            HasRequired(m => m.Medicament).WithMany(m => m.OrderDetailses).HasForeignKey(m => m.MedicamentId).WillCascadeOnDelete(true);

        }
    }
}
