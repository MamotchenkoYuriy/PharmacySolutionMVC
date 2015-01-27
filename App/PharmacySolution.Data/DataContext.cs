using System;
using System.Data.Entity;
using PharmacySolution.Data.Mapping;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using PharmacySolution.Core;

namespace PharmacySolution.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<MedicamentPriceHistory> MedicamentPriceHistories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetailses { get; set; }
        public DbSet<Storage> Storages { get; set; }

        public DataContext()
            : base(ConfigurationConnectionString.GetConnectionString("PharmacyDbConnection"))
        {
            Database.SetInitializer(new DataBaseInitializer());
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var mapTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                               && type.BaseType.GetGenericTypeDefinition()
                               == typeof(EntityTypeConfiguration<>));

            foreach (var type in mapTypes)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);

            /*

            modelBuilder.Configurations.Add(new PharmacyMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new OrderDetailsMap());
            modelBuilder.Configurations.Add(new StorageMap());
            modelBuilder.Configurations.Add(new MedicamentMap());
            modelBuilder.Configurations.Add(new MedicamentPriceHistoryMap());*/
        }
    }
}
