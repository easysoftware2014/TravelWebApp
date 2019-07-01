using System.Web.Mvc;
using Antlr.Runtime.Misc;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Models;

namespace TravelWebApp.Controllers
{
    [Authorize]
    public class BookingManagementController : Controller
    {
        public BookingManagementController()
        {}
        public ActionResult Index()
        {
            var user = (User)Session["User"];
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