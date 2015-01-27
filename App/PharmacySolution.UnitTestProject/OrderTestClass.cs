using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacySolution.Contracts.Manager;
using PharmacySolution.Core;
using PharmacySolution.IOC;

namespace UnitTestProject
{
    [TestClass]
    public class OrderTestClass
    {
        /*Я так думаю что Order ни в коем случае изменяться не будет, разве что удаляться
         * Поэтому метод изменения не вижу смысла писать
         * Ну ради прикола напишу что изменилась дата :-) 
         */

        [TestMethod]
        public void OrderAddNewRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Order>>();
            var currentCountRecords = manager.FindAll().Count();
            var firstPharmacy = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Pharmacy>>().FindAll().FirstOrDefault();
            if(firstPharmacy==null){throw new AssertFailedException();}
            manager.Add(new Order()
                {
                    OperationDate = DateTime.Now,
                    OperationType = OperationType.Purchase,
                    Pharmacy = firstPharmacy
                });
            manager.SaveChanges();
            var newCountRecords = manager.FindAll().Count();
            Assert.AreEqual(currentCountRecords + 1, newCountRecords);
        }

        [TestMethod]
        public void OrderDeleteRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Order>>();
            var firstPharmacy = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Pharmacy>>().FindAll().FirstOrDefault();
            manager.Add(new Order()
                {
                    OperationDate = DateTime.Now,
                    OperationType = OperationType.Purchase,
                    Pharmacy = firstPharmacy
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
        public void OrderEditRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Order>>();
            var firstPharmacy = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Pharmacy>>().FindAll().FirstOrDefault();
            manager.Add(new Order()
                {
                    OperationDate = DateTime.Now,
                    OperationType = OperationType.Purchase,
                    Pharmacy = firstPharmacy
                });
            manager.SaveChanges();
            var firstRecord = manager.FindAll().FirstOrDefault();
            if (firstRecord == null)
            {
                throw new AssertFailedException();
            }
            var id = firstRecord.Id;
            var oldDate = firstRecord.OperationDate;
            var newDate = DateTime.Now;
            firstRecord.OperationDate = newDate;
            manager.SaveChanges();
            var changedRecord = manager.Find(m => m.Id == id).FirstOrDefault();
            Assert.IsNotNull(changedRecord);
            Assert.AreEqual(newDate, changedRecord.OperationDate);
            changedRecord.OperationDate = oldDate;
            manager.SaveChanges();
            changedRecord = manager.Find(m => m.OperationDate == oldDate).FirstOrDefault();
            if (changedRecord == null)
            {
                throw new AssertFailedException();
            }
            Assert.AreEqual(changedRecord.OperationDate, oldDate);
        }
    }
}
