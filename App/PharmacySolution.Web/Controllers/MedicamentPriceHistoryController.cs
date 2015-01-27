using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using PharmacySolution.Contracts.Manager;
using PharmacySolution.Core;
using PharmacySolution.Web.Core.Models;

namespace PharmacySolution.Web.Controllers
{
    public class MedicamentPriceHistoryController : Controller
    {
        private readonly IManager<MedicamentPriceHistory> _medicamentPriceHistoryManager;
        private readonly IManager<Medicament> _medicamentManager;

        public MedicamentPriceHistoryController(IManager<MedicamentPriceHistory> medicamentPriceHistoryManager, IManager<Medicament> medicamentManager)
        {
            _medicamentPriceHistoryManager = medicamentPriceHistoryManager;
            _medicamentManager = medicamentManager;
        }



        //
        // GET: /MedicamentPriceHistory/
        public ActionResult Index()
        {
            var list = _medicamentPriceHistoryManager.FindAll();
            var listMedicaments = new List<SelectListItem> {new SelectListItem {Text = "All", Value = "0"}};
            listMedicaments.AddRange(from medicament in _medicamentManager.FindAll()
                                  select new SelectListItem() { Text = medicament.Name, Value = medicament.Id.ToString() });
            ViewBag.Medicaments = listMedicaments.ToList();
            return View(Mapper.Map<IQueryable<MedicamentPriceHistory>, List<MedicamentPriceHistoryViewModel>>(list));
        }

        public PartialViewResult GetTablePartialView(int id = 0)
        {
            if (id == 0)
            {
                return PartialView(
                    Mapper.Map<IQueryable<MedicamentPriceHistory>, 
                    List<MedicamentPriceHistoryViewModel>>
                    (_medicamentPriceHistoryManager.FindAll()));
            }
            var list = from medicamentPriceHistory in _medicamentPriceHistoryManager.FindAll()
                       join medicament in _medicamentManager.FindAll()
                              on medicamentPriceHistory.MedicamentId equals medicament.Id
                              where medicament.Id == id
                       select medicamentPriceHistory;
            return PartialView(Mapper.Map<IQueryable<MedicamentPriceHistory>, List<MedicamentPriceHistoryViewModel>>(list));
        }

        //
        // GET: /MedicamentPriceHistory/Details/5
        public ActionResult Details(int id)
        {
            var entity = _medicamentPriceHistoryManager.GetByPrimaryKey(id);
            return View(Mapper.Map<MedicamentPriceHistory, MedicamentPriceHistoryViewModel>(entity));
        }

        //
        // GET: /MedicamentPriceHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MedicamentPriceHistory/Create
        [HttpPost]
        public ActionResult Create(MedicamentPriceHistoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                var entity = Mapper.Map<MedicamentPriceHistoryViewModel, MedicamentPriceHistory>(model);
                _medicamentPriceHistoryManager.Add(entity);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Error to create new record");
                return View(model);
            }
        }

        //
        // GET: /MedicamentPriceHistory/Edit/5
        public ActionResult Edit(int id)
        {
            var entity = _medicamentPriceHistoryManager.GetByPrimaryKey(id);
            if (entity != null) return View(Mapper.Map<MedicamentPriceHistory, MedicamentPriceHistoryViewModel>(entity));
            ModelState.AddModelError("", "Запись с введенным ID не найденна");
            return View(new MedicamentPriceHistoryViewModel());
        }

        //
        // POST: /MedicamentPriceHistory/Edit/5
        [HttpPost]
        public ActionResult Edit(MedicamentPriceHistoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                var entity = _medicamentPriceHistoryManager.GetByPrimaryKey(model.Id);
                entity.ModifiedDate = model.ModifiedDate;
                entity.Price = model.Price;
                entity.Medicament = _medicamentManager.GetByPrimaryKey(model.MedicamentId);
                entity.MedicamentId = model.MedicamentId;
                _medicamentPriceHistoryManager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Additing new record Error!!!");
                return View();
            }
        }

        //
        // GET: /MedicamentPriceHistory/Delete/5
        public ActionResult Delete(int id)
        {
            var entity = _medicamentPriceHistoryManager.GetByPrimaryKey(id);
            if (entity != null)
                return View(Mapper.Map<MedicamentPriceHistory, MedicamentPriceHistoryViewModel>(entity));
            ModelState.AddModelError("", "Entity with this ID not found");
            return View(new MedicamentPriceHistoryViewModel());
        }

        //
        // POST: /MedicamentPriceHistory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var entity = _medicamentPriceHistoryManager.GetByPrimaryKey(id);
                if (entity != null)
                {
                    _medicamentPriceHistoryManager.Remove(entity);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Delete record error!!!");
                return View();
            }
            catch
            {
                ModelState.AddModelError("", "Delete record error!!!");
                return View();
            }
        }
    }
}
