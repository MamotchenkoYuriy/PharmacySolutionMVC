using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacySolution.Core;

namespace PharmacySolution.Data
{
    public class DataBaseInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        public DataBaseInitializer()
        {
        }

        protected override void Seed(DataContext context)
        {

            #region Creating Pharmacy Enteties

            for (var i = 0; i < 10; i++)
            {
                context.Pharmacies.Add(new Pharmacy()
                {
                    Address = "TestAddress" + i, 
                    Number = "050173384" + i, 
                    OpenDate = DateTime.Now,
                    PhoneNumber = "050173384" + i
                });
            }
            context.SaveChanges();

            #endregion

            #region Creating Medicament Enteties

            for (var i = 0; i < 10; i++)
            {
                context.Medicaments.Add(new Medicament()
                {
                    Description = "Description " + i,
                    Name = "TestName" + i,
                    Price = (decimal)(i * 100 - 50 + 18 /3),
                    SerialNumber = "050173384" + i
                });
            }
            context.SaveChanges();

            #endregion

            #region Creating MedicamentPriceHistory Enteties

            var medicament = context.Medicaments.FirstOrDefault();
            for (var i = 0; i < 10; i++)
            {
                context.MedicamentPriceHistories.Add(new MedicamentPriceHistory()
                {
                    Medicament = medicament, 
                    Price = (decimal)100500, 
                    ModifiedDate = DateTime.Now
                });
            }
            context.SaveChanges();

            #endregion

            #region Creating Orders Enteties

            var pharmacy = context.Pharmacies.FirstOrDefault();
            for (var i = 0; i < 10; i++)
            {
                context.Orders.Add(new Order()
                {
                    Pharmacy = pharmacy, 
                    OperationDate = DateTime.Now, 
                    OperationType = OperationType.Purchase, 
                });
            }
            context.SaveChanges();

            #endregion

            #region Creating OrdersDetails Enteties

            var order = context.Orders.FirstOrDefault();
            var medicaments = context.Medicaments.ToList();
            for (var i = 0; i < 10; i++)
            {
                context.OrderDetailses.Add(new OrderDetails()
                {
                    Order = order, 
                    Medicament = medicaments[i], 
                    Count = 100500, 
                    UnitPrice = (decimal)100500 
                });
            }
            context.SaveChanges();

            #endregion

            #region Creating Storage Enteties

            pharmacy = context.Pharmacies.FirstOrDefault();
            medicaments = context.Medicaments.ToList();
            for (var i = 0; i < 10; i++)
            {
                context.Storages.Add(new Storage()
                {
                    Medicament = medicaments[i],
                    Count = (1000 + i),
                    Pharmacy = pharmacy
                });
            }
            context.SaveChanges();

            #endregion

        }
    }
}
