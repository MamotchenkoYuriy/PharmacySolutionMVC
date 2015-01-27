using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using PharmacySolution.Contracts.Manager;
using PharmacySolution.Core;
using PharmacySolution.Web.Core.Models;

namespace PharmacySolution.Web.Controllers
{
    public class MedicamentController : Controller
    {
        private readonly IManager<Medicament> _medicamentManager;
        private readonly IManager<Pharmacy> _pharmacyManager;
        private readonly IManager<Storage> _storageManager;


        public MedicamentController(IManager<Medicament> medicamentManager, IManager<Pharmacy> pharmacyManager, IManager<Storage> storageManager)
        {
            _medicamentManager = medicamentManager;
            _pharmacyManager = pharmacyManager;
            _storageManager = storageManager;
        }


        //
        // GET: /Medicament/
        public ActionResult Index()
        {
            var list = new List<SelectListItem> {new SelectListItem {Text = "All", Value = "0"}};
            list.AddRange(from item in _pharmacyManager.FindAll()
                select new SelectListItem() {Text = item.Number, Value = item.Id.ToString()});
            ViewBag.Pharmacies = list;
            var listMedicament = Mapper.Map<IQueryable<Medicament>, List<MedicamentViewModel>>(_medicamentManager.FindAll());
            return View(listMedicament);
        }


        [HttpGet]
        public PartialViewResult GetTable(int id = 0)
        {
            if (id == 0)
            {
                return PartialView(Mapper.Map<IQueryable<Medicament>, List<MedicamentViewModel>>(_medicamentManager.FindAll()));
            }
            var listMedicament = from medicament in _medicamentManager.FindAll()
                join storage in _storageManager.FindAll() on medicament.Id equals storage.MedicamentId
                join pharmacy in _pharmacyManager.FindAll() on storage.PharmacyId equals pharmacy.Id
                where pharmacy.Id == id
                select medicament;
            return PartialView(Mapper.Map<IQueryable<Medicament>, List<MedicamentViewModel>>(listMedicament));
        }

        //
        // GET: /Medicament/Details/5
        public ActionResult Details(int id)
        {
            var entity = _medicamentManager.GetByPrimaryKey(id);
            return View(Mapper.Map<Medicament, MedicamentViewModel>(entity));
        }

        //
        // GET: /Medicament/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Medicament/Create
        [HttpPost]
        public ActionResult Create(MedicamentViewModel medicamentView)
        {
            if (!ModelState.IsValid) return View(medicamentView);
            try
            {
                var entity = Mapper.Map<MedicamentViewModel, Medicament>(medicamentView);
                _medicamentManager.Add(entity);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Возникла ошибка при добавлении сущьности!");
                return View(medicamentView);
            }
        }

        //
        // GET: /Medicament/Edit/5
        public ActionResult Edit(int id)
        {
            var medicament = _medicamentManager.GetByPrimaryKey(id);
            return View(Mapper.Map<Medicament, MedicamentViewModel>(medicament));
        }

        //
        // POST: /Medicament/Edit/5
        [HttpPost]
        public ActionResult Edit(MedicamentViewModel medicamentView)
        {
            if (!ModelState.IsValid) return View(medicamentView);
            try
            {
                var entityFromDb = _medicamentManager.GetByPrimaryKey(medicamentView.Id);
                entityFromDb.Name = medicamentView.Name;
                entityFromDb.Description = medicamentView.Description;
                entityFromDb.SerialNumber = medicamentView.SerialNumber;
                entityFromDb.Price = medicamentView.Price;
                //пока что не проверил эту часть
                var history = new MedicamentPriceHistory()
                {
                    Medicament = entityFromDb,
                    MedicamentId = entityFromDb.Id,
                    ModifiedDate = DateTime.Now,
                    Price = entityFromDb.Price
                };
                entityFromDb.MedicamentPriceHistories.Add(history);
                _medicamentManager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Возникла ошибка при изменении сущьности!");
                return View(medicamentView);
            }
        }

        //
        // GET: /Medicament/Delete/5
        public ActionResult Delete(int id)
        {
            var entity = _medicamentManager.GetByPrimaryKey(id);
            return View(Mapper.Map<Medicament, MedicamentViewModel>(entity));
        }

        //
        // POST: /Medicament/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var entity = _medicamentManager.GetByPrimaryKey(id);
                _medicamentManager.Remove(entity);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Вознизкла ошибка при удалении данных!");
                return View();
            }
        }
    }
}
