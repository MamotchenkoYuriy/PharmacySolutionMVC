using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PharmacySolution.Contracts.Manager;
using PharmacySolution.Core;
using PharmacySolution.Web.Core.Models;

namespace PharmacySolution.Web.Controllers
{
    public class StorageController : Controller
    {
        private readonly IManager<Pharmacy> _pharmacyManager;
        private readonly IManager<Medicament> _medicamentManager;
        private readonly IManager<Storage> _storageManager;

        public StorageController(IManager<Pharmacy> pharmacyManager, IManager<Medicament> medicamentManager, IManager<Storage> storageManager)
        {
            _pharmacyManager = pharmacyManager;
            _medicamentManager = medicamentManager;
            _storageManager = storageManager;
        }
        //
        // GET: /Storage/
        public ActionResult Index()
        {
            var list = _storageManager.FindAll();
            var listtoView = Mapper.Map<IQueryable<Storage>, List<StorageViewModel>>(list);
            return View(listtoView);
        }

        //
        // GET: /Storage/Details/5
        public ActionResult Details(int pharmacyId, int medicamentId)
        {
            var entity =
                _storageManager.Find(m => m.MedicamentId == medicamentId && m.PharmacyId == pharmacyId).FirstOrDefault();
            return View(Mapper.Map<Storage, StorageViewModel>(entity));
        }

        //
        // GET: /Storage/Create
        public ActionResult Create()
        {
            var listPharmacies = new SelectList(_pharmacyManager.FindAll(), "Id", "Number");
            ViewBag.Pharmacies = listPharmacies;
            var listMedicaments = new SelectList(_medicamentManager.FindAll(), "Id", "Name");
            ViewBag.Medicaments = listMedicaments;
            return View();
        }

        //
        // POST: /Storage/Create
        [HttpPost]
        public ActionResult Create(StorageCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var listPharmacies = new SelectList(_pharmacyManager.FindAll(), "Id", "Number");
                ViewBag.Pharmacies = listPharmacies;
                var listMedicaments = new SelectList(_medicamentManager.FindAll(), "Id", "Name");
                ViewBag.Medicaments = listMedicaments;
                return View();
            }
            try
            {
                var entity = Mapper.Map<StorageCreateViewModel, Storage>(model);
                _storageManager.Add(entity);
                _storageManager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                var listPharmacies = new SelectList(_pharmacyManager.FindAll(), "Id", "Number");
                ViewBag.Pharmacies = listPharmacies;
                var listMedicaments = new SelectList(_medicamentManager.FindAll(), "Id", "Name");
                ViewBag.Medicaments = listMedicaments;
                ModelState.AddModelError("error", "Adding new record error!");
                return View(model);
            }
        }

        //
        // GET: /Storage/Edit/5
        public ActionResult Edit(int pharmacyId, int medicamentId)
        {
            var listPharmacies = new SelectList(_pharmacyManager.FindAll(), "Id", "Number");
            ViewBag.Pharmacies = listPharmacies;
            var listMedicaments = new SelectList(_medicamentManager.FindAll(), "Id", "Name");
            ViewBag.Medicaments = listMedicaments;
            var entity =
                _storageManager.Find(m => m.MedicamentId == medicamentId && m.PharmacyId == pharmacyId).FirstOrDefault();
            return View(Mapper.Map<Storage, StorageCreateViewModel>(entity));
        }

        //
        // POST: /Storage/Edit/5
        [HttpPost]
        public ActionResult Edit(StorageCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var listPharmacies = new SelectList(_pharmacyManager.FindAll(), "Id", "Number");
                ViewBag.Pharmacies = listPharmacies;
                var listMedicaments = new SelectList(_medicamentManager.FindAll(), "Id", "Name");
                ViewBag.Medicaments = listMedicaments;
                return View(model);
            }
            try
            {
                var entity =
                _storageManager.Find(m => m.MedicamentId == model.MedicamentId && m.PharmacyId == model.PharmacyId).FirstOrDefault();
                entity.Count = model.Count;
                _storageManager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                var listPharmacies = new SelectList(_pharmacyManager.FindAll(), "Id", "Number");
                ViewBag.Pharmacies = listPharmacies;
                var listMedicaments = new SelectList(_medicamentManager.FindAll(), "Id", "Name");
                ViewBag.Medicaments = listMedicaments;
                ModelState.AddModelError("", "Additing record error!!!");
                return View();
            }
        }

        //
        // GET: /Storage/Delete/5
        public ActionResult Delete(int pharmacyId, int medicamentId)
        {
            var entity =
                _storageManager.Find(m => m.MedicamentId == medicamentId && m.PharmacyId == pharmacyId).FirstOrDefault();
            return View(Mapper.Map<Storage, StorageViewModel>(entity));
        }

        //
        // POST: /Storage/Delete/5
        [HttpPost]
        public ActionResult Delete(int pharmacyId, int medicamentId, FormCollection collection)
        {
            try
            {
                var entity =
                _storageManager.Find(m => m.MedicamentId == medicamentId && m.PharmacyId == pharmacyId).FirstOrDefault();
                _storageManager.Remove(entity);
                _storageManager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Deleting record error!!!");
                return View();
            }
        }
    }
}
