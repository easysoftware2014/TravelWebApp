using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Models;
using TravelWebApp.Service.Contracts;
using TravelWebApp.Service.Services;

namespace TravelWebApp.Controllers
{
    public class BudgetController : Controller
    {
        private readonly IBudgetService _budgetService;

        public BudgetController()
        {
            _budgetService = new BudgetService();
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Budget/Details/5
        public ActionResult Details(int id)
        {
            return View(" ");
        }

        // GET: Budget/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BudgetModel model)
        {
            try
            {
                var entity = new Budget
                {
                    Amount = model.Amount,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    ValidTo = model.ValidTo,
                    ValidFrom = model.ValidFrom
                };

                _budgetService.Save(entity);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    var exception = e.InnerException.Message;
                }

                return View("Index");
            }
        }

        // GET: Budget/Edit/5
        public ActionResult Edit(int id)
        {
            return View(" ");
        }

        // POST: Budget/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(" ");
            }
        }

        // GET: Budget/Delete/5
        public ActionResult Delete(int id)
        {
            return View(" ");
        }

        // POST: Budget/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(" ");
            }
        }
    }
}
