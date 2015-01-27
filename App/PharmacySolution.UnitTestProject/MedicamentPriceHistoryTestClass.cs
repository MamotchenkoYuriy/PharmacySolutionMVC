using System;
using System.CodeDom;
using System.Linq;
using PharmacySolution.IOC;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacySolution.Contracts.Manager;
using PharmacySolution.Core;

namespace UnitTestProject
{
    [TestClass]
    public class MedicamentPriceHistoryTestClass
    {
        [TestMethod]
        public void MedicamentPriceHistoryAddNewRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<MedicamentPriceHistory>>();
            var firstMedicament = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Medicament>>().FindAll().FirstOrDefault();
            if (firstMedicament == null) { throw new AssertFailedException();}
            var currentCountRecords = manager.FindAll().Count();
            manager.Add(
                new MedicamentPriceHistory()
                {
                    Medicament = firstMedicament, 
                    ModifiedDate = DateTime.Now, 
                    Price = new Random().Next(100500)
                });
            manager.SaveChanges();
            var newCountRecords = manager.FindAll().Count();
            Assert.AreEqual(currentCountRecords + 1, newCountRecords);
        }

        [TestMethod]
        public void MedicamentPriceHistoryDeleteRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<MedicamentPriceHistory>>();
            var firstMedicament = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Medicament>>().FindAll().FirstOrDefault();
            if (firstMedicament == null) { throw new AssertFailedException(); }
            manager.Add(
                new MedicamentPriceHistory()
                {
                    Medicament = firstMedicament,
                    ModifiedDate = DateTime.Now,
                    Price = new Random().Next(100500)
                });
            manager.SaveChanges();
            var currentCountRecords = manager.FindAll().Count();
            var removeEntity = manager.FindAll().FirstOrDefault();
            if (removeEntity == null)
            {
                throw new AssertFailedException();
            }
            manager.Remove(removeEntity);
            manager.SaveChanges();
            var newCountRecords = manager.FindAll().Count();
            Assert.AreEqual(currentCountRecords - 1, newCountRecords);
        }

        [TestMethod]
        public void MedicamentPriceHistoryEditRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<MedicamentPriceHistory>>();
            var firstRecord = manager.FindAll().FirstOrDefault();
            if (firstRecord == null) { throw new AssertFailedException();}
            var id = firstRecord.Id;
            var oldPrice = firstRecord.Price;
            var newPrice = new Random().Next(100500);
            firstRecord.Price = newPrice;
            manager.SaveChanges();
            var changedRecord =
                manager.Find(m => m.Id == id && 
                    m.Price == newPrice).FirstOrDefault();
            Assert.IsNotNull(changedRecord);
            changedRecord.Price = oldPrice;
            manager.SaveChanges();
            changedRecord =
                manager.Find(m => m.Id == id &&
                    m.Price == newPrice).FirstOrDefault();
            Assert.IsNull(changedRecord);
        }
    }
}
