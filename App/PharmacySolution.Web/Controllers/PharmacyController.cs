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
    public class PharmacyController : Controller
    {
        private readonly IManager<Pharmacy> _manager;

        public PharmacyController(IManager<Pharmacy> manager)
        {
            _manager = manager;
        }


        //
        // GET: /Pharmacy/
        public ActionResult Index()
        {
            var list = Mapper.Map<IQueryable<Pharmacy>, List<PharmacyViewModel>>(_manager.FindAll());
            return View(list);
        }

        //
        // GET: /Pharmacy/Details/5
        public ActionResult Details(int id)
        {
            var entity = _manager.GetByPrimaryKey(id);
            var entityView = Mapper.Map<Pharmacy, PharmacyViewModel>(entity);
            return View(entityView);
        }

        //
        // GET: /Pharmacy/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Pharmacy/Create
        [HttpPost]
        public ActionResult Create(PharmacyViewModel pharmacyView)
        {
            if (!ModelState.IsValid) return View(pharmacyView);
            try
            {
                var pharmacy = Mapper.Map<PharmacyViewModel, Pharmacy>(pharmacyView);
                _manager.Add(pharmacy);
                _manager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Ошибка добавления данных");
                return View(pharmacyView);
            }
        }

        //
        // GET: /Pharmacy/Edit/5
        public ActionResult Edit(int id)
        {
            var entityFomDb = _manager.GetByPrimaryKey(id);
            var entity = Mapper.Map<Pharmacy, PharmacyViewModel>(entityFomDb);
            return View(entity);
        }

        //
        // POST: /Pharmacy/Edit/5
        [HttpPost]
        public ActionResult Edit(PharmacyViewModel pharmacyView)
        {
            if (!ModelState.IsValid) return View(pharmacyView);
            try
            {
                // По другому не получается это сделать :-((((
                //var entity = Mapper.Map<PharmacyView, Pharmacy>(pharmacyView);
                var entity = _manager.GetByPrimaryKey(pharmacyView.Id);
                entity.Address = pharmacyView.Address;
                entity.Number = pharmacyView.Number;
                entity.OpenDate = pharmacyView.OpenDate;
                entity.PhoneNumber = pharmacyView.PhoneNumber;
                _manager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "Возникла какая то ошибка при изменении данных");
                return View(pharmacyView);
            }
        }

        //
        // GET: /Pharmacy/Delete/5
        public ActionResult Delete(int id)
        {
            var entityFroDb = _manager.GetByPrimaryKey(id);
            var entity = Mapper.Map<Pharmacy, PharmacyViewModel>(entityFroDb);
            return View(entity);
        }

        //
        // POST: /Pharmacy/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var entity = _manager.GetByPrimaryKey(id);
            try
            {
                _manager.Remove(entity);
                _manager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Возникла ошибка при удалении записи!");
                return View();
            }
        }
    }
}
