using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using Castle.Components.DictionaryAdapter;
using PharmacySolution.Contracts.Manager;
using PharmacySolution.Core;
using PharmacySolution.Web.Core.Models;
using PharmacySolution.Web.Core.Validators;

namespace PharmacySolution.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IManager<Pharmacy> _pharmacyManager;
        private readonly IManager<Order> _orderManager;

        public OrderController(IManager<Pharmacy> pharmacyManager, IManager<Order> orderManager)
        {
            _pharmacyManager = pharmacyManager;
            _orderManager = orderManager;
        }



        //
        // GET: /MedicamentPriceHistory/
        public ActionResult Index()
        {
            var list = _orderManager.FindAll();
            var listPharmacies = new List<SelectListItem> { new SelectListItem { Text = "All", Value = "0" } };
            listPharmacies.AddRange(from pharmacy in _pharmacyManager.FindAll()
                                    select new SelectListItem() { Text = pharmacy.Number, Value = pharmacy.Id.ToString() });
            ViewBag.Orders = listPharmacies.ToList();
            return View(Mapper.Map<IQueryable<Order>, List<OrderViewModel>>(list));
        }


        public PartialViewResult GetTablePartialView(int id = 0)
        {
            if (id == 0)
            {
                return PartialView(
                    Mapper.Map<IQueryable<Order>,
                    List<OrderViewModel>>
                    (_orderManager.FindAll()));
            }
            var list = from order in _orderManager.FindAll()
                       join pharmacy in _pharmacyManager.FindAll()
                              on order.PharmacyId equals pharmacy.Id
                       where pharmacy.Id == id
                       select order;
            return PartialView(Mapper.Map<IQueryable<Order>, List<OrderViewModel>>(list));
        }

        //
        // GET: /Order/Details/5
        public ActionResult Details(int id)
        {
            var entity = _orderManager.GetByPrimaryKey(id);
            if (entity == null) return View();
            return View(Mapper.Map<Order, OrderViewModel>(entity));
        }

        //
        // GET: /Order/Create
        public ActionResult Create()
        {
            SelectList listPharmacies = new SelectList(_pharmacyManager.FindAll(), "Id", "Number");
            ViewBag.Pharmacies = listPharmacies;
            SelectList listTypes = new SelectList(new List<object>() { new { Id = 2, Value = "Purchase" }, new { Id = 1, Value = "Sale" } }, "Id", "Value");
            ViewBag.OperationTypes = listTypes;
            return View(new OrderViewModel(){OperationDate = DateTime.Now});
        }

        //
        // POST: /Order/Create
        [HttpPost]
        public ActionResult Create(OrderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var listPharmacies = new SelectList(_pharmacyManager.FindAll(), "Id", "Number");
            ViewBag.Pharmacies = listPharmacies;
            var listTypes = new SelectList(new List<object>() { new { Id = 2, Value = "Purchase" }, new { Id = 1, Value = "Sale" } }, "Id", "Value");
            ViewBag.OperationTypes = listTypes;
            try
            {
                _orderManager.Add(Mapper.Map<OrderViewModel, Order>(model));
                _orderManager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Adding new record error!");
                return View(model);
            }
        }

        //
        // GET: /Order/Edit/5
        public ActionResult Edit(int id)
        {
            SelectList listPharmacies = new SelectList(_pharmacyManager.FindAll(), "Id", "Number");
            ViewBag.Pharmacies = listPharmacies;
            SelectList listTypes = new SelectList(new List<object>() { new { Id = 2, Value = "Purchase" }, new { Id = 1, Value = "Sale" } }, "Id", "Value");
            ViewBag.OperationTypes = listTypes;
            var entity = _orderManager.GetByPrimaryKey(id);
            if (entity != null) return View(Mapper.Map<Order, OrderViewModel>(entity));
            ModelState.AddModelError("", "Запись с введенным ID не найденна");
            return View(new OrderViewModel());
        }

        //
        // POST: /Order/Edit/5
        [HttpPost]
        public ActionResult Edit(OrderViewModel model)
        {
            SelectList listPharmacies = new SelectList(_pharmacyManager.FindAll(), "Id", "Number");
            ViewBag.Pharmacies = listPharmacies;
            SelectList listTypes = new SelectList(new List<object>() { new { Id = 2, Value = "Purchase" }, new { Id = 1, Value = "Sale" } }, "Id", "Value");
            ViewBag.OperationTypes = listTypes;
            if (!ModelState.IsValid) return View(model);
            try
            {
                // По другому не получается это сделать :-((((
                //var entity = Mapper.Map<PharmacyView, Pharmacy>(pharmacyView);
                var entity = _orderManager.GetByPrimaryKey(model.Id);
                entity.OperationDate = model.OperationDate;
                entity.OperationType = model.OperationType;
                entity.PharmacyId = model.PharmacyId;
                entity.Pharmacy = _pharmacyManager.GetByPrimaryKey(model.PharmacyId);
                _orderManager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "Something wrong with editing record!");
                return View(model);
            }
        }

        //
        // GET: /Order/Delete/5
        public ActionResult Delete(int id)
        {
            var entity = _orderManager.GetByPrimaryKey(id);
            return entity != null ? View(Mapper.Map<Order, OrderViewModel>(entity)) : View(new Order());
        }

        //
        // POST: /Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var entity = _orderManager.GetByPrimaryKey(id);
                if (entity != null)
                {
                    _orderManager.Remove(entity);
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
