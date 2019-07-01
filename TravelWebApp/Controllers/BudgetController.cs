using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Models;
using TravelWebApp.Service.Contracts;
using TravelWebApp.Service.Services;

namespace TravelWebApp.Controllers
{
    [Authorize]
    public class BudgetController : Controller
    {
        private readonly IBudgetService _budgetService;
        private readonly IUserService _userService;

        public BudgetController()
        {
            _budgetService = new BudgetService();
            _userService = new UserService();
        }
        public ActionResult Index()
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var model = new List<BudgetModel>();

            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var id = Convert.ToInt32(authTicket?.UserData);
                var user = _userService.Get(id);
                Session["user"] = user;
                var budget = _budgetService.GetBudgetByUserId(user);

                if (budget != null)
                {
                    model.Add(new BudgetModel(budget));
                }

            }

            return View(model);
        }

        public ActionResult Details(int id)
        {

            var budget = _budgetService.Get(id);
            var model = new BudgetModel(budget);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BudgetModel model)
        {
            try
            {

                var user = Session["user"] as User;

                var entity = new Budget
                {
                    Amount = model.Amount,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    User = user
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

        public ActionResult Edit(int id)
        {
            var budget = _budgetService.Get(id);
            var model = new BudgetModel(budget);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BudgetModel model)
        {
            try
            {
                // TODO: Add update logic here
                var id = model.Id;
                var budget = _budgetService.Get(id);
                var user = Session["user"] as User;
                _budgetService.Delete(budget);

                var newBudget = new Budget
                {
                    Amount = model.Amount,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    User = user
                };

                _budgetService.SaveOrUpdate(newBudget);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var message = e.Message;
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
