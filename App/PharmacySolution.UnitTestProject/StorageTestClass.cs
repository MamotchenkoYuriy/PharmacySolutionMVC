using System.ComponentModel;
using System.Linq;
using PharmacySolution.IOC;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacySolution.Contracts.Manager;
using PharmacySolution.Contracts.Repository;
using PharmacySolution.Contracts.Validator;
using PharmacySolution.Core;

namespace UnitTestProject
{
    [TestClass]
    public class StorageTestClass
    {
        [TestMethod]
        public void StorageAddNewRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Storage>>();
            var currentCountRecords = manager.FindAll().Count();
            var firstMedicament = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Medicament>>().FindAll().FirstOrDefault();
            var firstPharmacy = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Pharmacy>>().FindAll().FirstOrDefault();
            if (firstPharmacy == null || firstMedicament == null)
            {
                throw new AssertFailedException();
            }
            var existingEntity = manager
                .FindAll()
                .FirstOrDefault(m => m.MedicamentId == firstMedicament.Id && m.PharmacyId == firstPharmacy.Id);
            if (existingEntity != null)
            {
                return;
            }

            manager.Add(
                new Storage()
                {
                    Count = 10,
                    MedicamentId = firstMedicament.Id,
                    PharmacyId = firstPharmacy.Id,
                });
            manager.SaveChanges();
            var newCountRecords = manager.FindAll().Count();
            Assert.AreEqual(currentCountRecords + 1, newCountRecords);
        }

        [TestMethod]
        public void OrderDetailsesDeleteRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Storage>>();
            var firstMedicament = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Medicament>>().FindAll().FirstOrDefault();
            var firstPharmacy = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Pharmacy>>().FindAll().FirstOrDefault();
            if (firstPharmacy == null || firstMedicament == null)
            {
                throw new AssertFailedException();
            }

            manager.Add(
                new Storage()
                {
                    Count = 10,
                    MedicamentId = firstMedicament.Id,
                    PharmacyId = firstPharmacy.Id,
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
        public void OrderDetailsesEditRecordTest()
        {
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Storage>>();
            var firstMedicament = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Medicament>>().FindAll().FirstOrDefault();
            var firstPharmacy = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Pharmacy>>().FindAll().FirstOrDefault();
            if (firstPharmacy == null || firstMedicament == null)
            {
                throw new AssertFailedException();
            }

            manager.Add(
                new Storage()
                {
                    Count = 10,
                    MedicamentId = firstMedicament.Id,
                    PharmacyId = firstPharmacy.Id,
                });
            manager.SaveChanges();
            var firstRecord = manager.FindAll().FirstOrDefault();
            if (firstRecord == null)
            {
                throw new AssertFailedException();
            }
            var medicamentId = firstRecord.MedicamentId;
            var pharmacyId = firstRecord.PharmacyId;

            var oldCount = firstRecord.Count;
            var newCount = 100500;
            firstRecord.Count = newCount;
            manager.SaveChanges();
            var changedRecord = manager.Find(m => m.MedicamentId == medicamentId && m.PharmacyId == pharmacyId).FirstOrDefault();
            Assert.IsNotNull(changedRecord);
            Assert.AreEqual(newCount, changedRecord.Count);
            changedRecord.Count = oldCount;
            manager.SaveChanges();
            changedRecord = manager.Find(m => m.MedicamentId == medicamentId && m.PharmacyId == pharmacyId).FirstOrDefault();
            if (changedRecord == null)
            {
                throw new AssertFailedException();
            }
            Assert.AreEqual(changedRecord.Count, oldCount);
        }
    }
}