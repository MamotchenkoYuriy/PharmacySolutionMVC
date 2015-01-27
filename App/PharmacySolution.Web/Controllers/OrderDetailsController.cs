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
    public class OrderDetailsController : Controller
    {
        private readonly IManager<Order> _orderManager; 
        private readonly IManager<OrderDetails> _orderDetailsManager;
        private readonly IManager<Medicament> _medicamentManager;

        public OrderDetailsController(IManager<Order> orderManager, IManager<OrderDetails> orderDetailsManager, IManager<Medicament> medicamentManager)
        {
            _orderManager = orderManager;
            _orderDetailsManager = orderDetailsManager;
            _medicamentManager = medicamentManager;
        }


        //
        // GET: /OrderDetails/
        public ActionResult Index()
        {
            var list = _orderDetailsManager.FindAll();
            var listPharmacies = new List<SelectListItem> { new SelectListItem { Text = "All", Value = "0" } };
            listPharmacies.AddRange(from order in _orderManager.FindAll()
                                    select new SelectListItem() { Text = order.Id.ToString(), Value = order.Id.ToString() });
            ViewBag.Orders = listPharmacies;
            var listtoView = Mapper.Map<IQueryable<OrderDetails>, List<OrderDetailsListViewModel>>(list);
            return View(listtoView);
        }

        public PartialViewResult GetTablePartialView(int id = 0)
        {
            if (id == 0)
            {
                return PartialView(
                    Mapper.Map<IQueryable<OrderDetails>,
                    List<OrderDetailsListViewModel>>
                    (_orderDetailsManager.FindAll()));
            }
            var list = from order in _orderManager.FindAll()
                       join orderDetails  in _orderDetailsManager.FindAll()
                              on order.Id equals orderDetails.OrderId
                       where order.Id == id
                       select orderDetails;
            return PartialView(Mapper.Map<IQueryable<OrderDetails>, List<OrderDetailsListViewModel>>(list));
        }

        //
        // GET: /OrderDetails/Details/5
        public ActionResult Details(int orderId, int medicamentId)
        {
            var entity =
                _orderDetailsManager.Find(m => m.OrderId == orderId && m.MedicamentId == medicamentId).FirstOrDefault();
            var toView = Mapper.Map<OrderDetails, OrderDetailsListViewModel>(entity);
            return View(toView);
        }

        //
        // GET: /OrderDetails/Create
        public ActionResult Create()
        {
            var listOrders = new SelectList(_orderManager.FindAll(), "Id", "Id");
            ViewBag.Orders = listOrders;
            var listMedicaments = new SelectList(_medicamentManager.FindAll(), "Id", "Name");
            ViewBag.Medicaments = listMedicaments;
            return View();
        }

        //
        // POST: /OrderDetails/Create
        [HttpPost]
        public ActionResult Create(OrderDetailsCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var listOrders = new SelectList(_orderManager.FindAll(), "Id", "Id");
                ViewBag.Orders = listOrders;
                var listMedicaments = new SelectList(_medicamentManager.FindAll(), "Id", "Name");
                ViewBag.Medicaments = listMedicaments;
                return View(model);
            }
            try
            {
                var entity = Mapper.Map<OrderDetailsCreateViewModel, OrderDetails>(model);
                _orderDetailsManager.Add(entity);
                _orderDetailsManager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                var listOrders = new SelectList(_orderManager.FindAll(), "Id", "Id");
                ViewBag.Orders = listOrders;
                var listMedicaments = new SelectList(_medicamentManager.FindAll(), "Id", "Name");
                ViewBag.Medicaments = listMedicaments;
                ModelState.AddModelError("", "Adding new record error!");
                return View();
            }
        }

        //
        // GET: /OrderDetails/Edit/5
        public ActionResult Edit(int orderId, int medicamentId)
        {
            var listOrders = new SelectList(_orderManager.FindAll(), "Id", "Id");
            ViewBag.Orders = listOrders;
            var listMedicaments = new SelectList(_medicamentManager.FindAll(), "Id", "Name");
            ViewBag.Medicaments = listMedicaments;
            var entity =
                _orderDetailsManager.Find(m => m.OrderId == orderId && m.MedicamentId == medicamentId).FirstOrDefault();
            var toView = Mapper.Map<OrderDetails, OrderDetailsCreateViewModel>(entity);
            return View(toView);
        }

        //
        // POST: /OrderDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(OrderDetailsCreateViewModel model )
        {
            if (!ModelState.IsValid)
            {
                var listOrders = new SelectList(_orderManager.FindAll(), "Id", "Id");
                ViewBag.Orders = listOrders;
                var listMedicaments = new SelectList(_medicamentManager.FindAll(), "Id", "Name");
                ViewBag.Medicaments = listMedicaments;
                return View(model);
            }
            try
            {
                var entity = _orderDetailsManager.Find(m=>m.OrderId == model.OrderId && m.MedicamentId == model.MedicamentId).FirstOrDefault();
                entity.OrderId = model.OrderId;
                entity.Medicament = _medicamentManager.GetByPrimaryKey(model.MedicamentId);
                entity.MedicamentId = model.MedicamentId;
                entity.Count = model.Count;
                entity.UnitPrice = model.UnitPrice;
                _orderDetailsManager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                var listOrders = new SelectList(_orderManager.FindAll(), "Id", "Id");
                ViewBag.Orders = listOrders;
                var listMedicaments = new SelectList(_medicamentManager.FindAll(), "Id", "Name");
                ViewBag.Medicaments = listMedicaments;
                ModelState.AddModelError("", "Adding new record error!");
                return View(model);
            }
        }

        //
        // GET: /OrderDetails/Delete/5
        public ActionResult Delete(int orderId, int medicamentId)
        {
            var entity =
                _orderDetailsManager.Find(m => m.OrderId == orderId && m.MedicamentId == medicamentId).FirstOrDefault();
            var toView = Mapper.Map<OrderDetails, OrderDetailsListViewModel>(entity);
            return View(toView);
        }

        //
        // POST: /OrderDetails/Delete/5
        [HttpPost]
        public ActionResult Delete(int orderId, int medicamentId, FormCollection collection)
        {
            var entity =
                _orderDetailsManager.Find(m => m.OrderId == orderId && m.MedicamentId == medicamentId).FirstOrDefault();
            try
            {
                _orderDetailsManager.Remove(entity);
                _orderDetailsManager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
                //return View(Mapper.Map<OrderDetails, OrderDetailsListViewModel>(entity));
            }
        }
    }
}
