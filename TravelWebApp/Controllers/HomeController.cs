using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using TravelWebApp.Models;

namespace TravelWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _path = ConfigurationManager.AppSettings["Cities"];
        [OutputCache(Duration = 6000, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var path = _path;
            var model = new CityModel();

            string jsonFromFile;
            using (var reader = new StreamReader(path))
            {
                jsonFromFile = reader.ReadToEnd();
            }

            var file = jsonFromFile;
            var serialized = JsonConvert.DeserializeObject<List<CitiesModel>>(file);
            ViewBag.Cities = serialized;
            
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Flight()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Accommodation()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return RedirectToAction("Login","User");
        }
        public ActionResult Register()
        {
            return RedirectToAction("Register","User");
        }
        
    }
}