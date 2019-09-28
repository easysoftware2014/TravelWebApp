using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelWebApp.Controllers
{
    public class AccommodationController : Controller
    {
        // GET: Accommodation
        public ActionResult Index()
        {
            return View("Accommodation");
        }
    }
}