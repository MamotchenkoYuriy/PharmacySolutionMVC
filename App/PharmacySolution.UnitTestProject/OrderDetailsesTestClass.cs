using System;
using System.Linq;
using PharmacySolution.IOC;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacySolution.Contracts.Manager;
using PharmacySolution.Core;

namespace UnitTestProject
{
    [TestClass]
    public class OrderDetailsesTestClass
    {
        [TestMethod]
        public void OrderDetailsesAddNewRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<OrderDetails>>();
            var currentCountRecords = manager.FindAll().Count();
            var firstMedicament = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Medicament>>().FindAll().FirstOrDefault();
            var firstOrder = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Order>>().FindAll().FirstOrDefault();
            if (firstOrder == null || firstMedicament == null)
            {
                throw new AssertFailedException();
            }

            manager.Add(
                new OrderDetails()
                {
                    Count = 10,
                    MedicamentId = firstMedicament.Id,
                    OrderId = firstOrder.Id,
                    UnitPrice = (decimal)2015.02
                });
            manager.SaveChanges();
            var newCountRecords = manager.FindAll().Count();
            Assert.AreEqual(currentCountRecords + 1, newCountRecords);
        }

        [TestMethod]
        public void OrderDetailsesDeleteRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<OrderDetails>>();
            var firstMedicament = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Medicament>>().FindAll().FirstOrDefault();
            var firstOrder = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Order>>().FindAll().FirstOrDefault();
            if (firstOrder == null || firstMedicament == null)
            {
                throw new AssertFailedException();
            }

            manager.Add(
                new OrderDetails()
                {
                    Count = 10,
                    MedicamentId = firstMedicament.Id,
                    OrderId = firstOrder.Id,
                    UnitPrice = (decimal)2015.02
                });
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
        public void OrderDetailsesEditRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<OrderDetails>>();
            var firstMedicament = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Medicament>>().FindAll().FirstOrDefault();
            var firstOrder = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Order>>().FindAll().FirstOrDefault();
            if (firstOrder == null || firstMedicament == null)
            {
                throw new AssertFailedException();
            }

            manager.Add(
                new OrderDetails()
                {
                    Count = 10,
                    MedicamentId = firstMedicament.Id,
                    OrderId = firstOrder.Id,
                    UnitPrice = (decimal)2015.02
                });
            var firstRecord = manager.FindAll().FirstOrDefault();
            if (firstRecord == null)
            {
                throw new AssertFailedException();
            }
            var medicamentId = firstRecord.MedicamentId;
            var orderId = firstRecord.OrderId;

            var oldCount = firstRecord.Count;
            var newCount = 100500;
            firstRecord.Count = newCount;
            manager.SaveChanges();
            var changedRecord =
                manager.Find(m => m.MedicamentId == medicamentId && m.OrderId == orderId).FirstOrDefault();
            Assert.IsNotNull(changedRecord);
            Assert.AreEqual(newCount, changedRecord.Count);
            changedRecord.Count = oldCount;
            manager.SaveChanges();
            changedRecord =
                manager.Find(m => m.MedicamentId == medicamentId && m.OrderId == orderId).FirstOrDefault();
            if (changedRecord == null)
            {
                throw new AssertFailedException();
            }
            Assert.AreEqual(changedRecord.Count, oldCount);
        }
    }
}