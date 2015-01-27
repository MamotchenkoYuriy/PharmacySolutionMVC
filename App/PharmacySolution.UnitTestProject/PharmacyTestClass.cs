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
    public class PharmacyTestClass
    {
        [TestMethod]
        public void PharmacyAddNewRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Pharmacy>>();
            var currentCountRecords = manager.FindAll().Count();
            manager.Add(new Pharmacy() { Address = Guid.NewGuid().ToString(), Number = Guid.NewGuid().ToString(), OpenDate = DateTime.Now, PhoneNumber = Guid.NewGuid().ToString() });
            manager.SaveChanges();
            var newCountRecords = manager.FindAll().Count();
            Assert.AreEqual(currentCountRecords + 1, newCountRecords);
        }

        [TestMethod]
        public void PharmacyDeleteRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Pharmacy>>();
            manager.Add(new Pharmacy() { Address = Guid.NewGuid().ToString(), Number = Guid.NewGuid().ToString(), OpenDate = DateTime.Now, PhoneNumber = Guid.NewGuid().ToString() });
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
        public void PharmacyEditRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Pharmacy>>();
            var firstRecord = manager.FindAll().FirstOrDefault();
            if (firstRecord == null) { throw new AssertFailedException();}
            var oldNumber = firstRecord.Number;
            firstRecord.Number = "TestNumber";
            manager.SaveChanges();
            var changedRecord =
                manager.Find(m => m.Number == "TestNumber").FirstOrDefault();
            Assert.IsNotNull(changedRecord);
            changedRecord.Number = oldNumber;
            manager.SaveChanges();
            changedRecord =
                manager.Find(m => m.Number == "TestNumber").FirstOrDefault();
            Assert.IsNull(changedRecord);
        }
    }
}
