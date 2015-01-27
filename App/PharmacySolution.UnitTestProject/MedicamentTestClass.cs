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
    public class MedicamentTestClass
    {
        [TestMethod]
        public void MedicamentAddNewRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Medicament>>();
            var currentCountRecords = manager.FindAll().Count();
            manager.Add(new Medicament()
                {
                    Description = Guid.NewGuid().ToString(),
                    Name = Guid.NewGuid().ToString(),
                    Price= (decimal) 100.25,
                    SerialNumber = Guid.NewGuid().ToString() 
                });
            manager.SaveChanges();
            var newCountRecords = manager.FindAll().Count();
            Assert.AreEqual(currentCountRecords + 1, newCountRecords);
        }

        [TestMethod]
        public void MedicamentDeleteRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Medicament>>();
            manager.Add(new Medicament()
                {
                    Description = Guid.NewGuid().ToString(),
                    Name = Guid.NewGuid().ToString(),
                    Price = (decimal)100.25,
                    SerialNumber = Guid.NewGuid().ToString()
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
        public void PharmacyEditRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Medicament>>();
            manager.Add(new Medicament()
                {
                    Description = Guid.NewGuid().ToString(),
                    Name = Guid.NewGuid().ToString(),
                    Price = (decimal)100.25,
                    SerialNumber = Guid.NewGuid().ToString()
                });
            manager.SaveChanges();
            var firstRecord = manager.FindAll().FirstOrDefault();
            if (firstRecord == null)
            {
                throw new AssertFailedException();
            }
            var oldName = firstRecord.Name;
            firstRecord.Name = "TestName";
            manager.SaveChanges();
            var changedRecord = manager.Find(m => m.Name == "TestName").FirstOrDefault();
            Assert.IsNotNull(changedRecord);
            changedRecord.Name = oldName;
            manager.SaveChanges();
            changedRecord = manager.Find(m => m.Name == "TestName").FirstOrDefault();
            Assert.IsNull(changedRecord);
        }
    }
}
