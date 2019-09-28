using System.Web.Mvc;

namespace TravelWebApp.Controllers
{
    public class FlightController : Controller
    {
        // GET: Flight
        public ActionResult Index()
        {
            return View("Flight");
        }
    }
}