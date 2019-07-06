using System;
using System.Web.Mvc;
using System.Web.Security;
using Antlr.Runtime.Misc;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Models;
using TravelWebApp.Service.Contracts;
using TravelWebApp.Service.Services;

namespace TravelWebApp.Controllers
{
    [Authorize]
    public class BookingManagementController : Controller
    {
        private readonly IUserService _userService;

        public BookingManagementController()
        {
            _userService = new UserService();
        }
        public ActionResult Index()
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var user = new User();

            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var id = Convert.ToInt32(authTicket?.UserData);
                user = (User)Session["User"] ?? _userService.Get(id);
                Session["User"] = user;
            }
            
            var model = new UserModel();

            if (user != null)
                model = new UserModel(user);

            return View(model);
        }

        public ActionResult UserProfile()
        {
            var user = (User)Session["User"];
            var model = new UserModel();

            if (user != null)
                model = new UserModel(user);

            return View(model);
        }

        public ActionResult Notification()
        {
            return View("Notification", new NotificationsModel());
        }

        public ActionResult Booking()
        {
            var model = new BookingModel();

            return View("Bookings", model);
        }

    }
}