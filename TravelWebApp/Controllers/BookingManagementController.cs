using System;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Models;
using TravelWebApp.Service.Contracts;
using TravelWebApp.Service.Services;
using unirest_net.http;

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

        public JsonResult GetPropertyList()
        {
            var url = "https://apidojo-booking-v1.p.rapidapi.com/properties/list?price_filter_currencycode=USD&travel_purpose=leisure&categories_filter=price%3A%3A9-40%2Cfree_cancellation%3A%3A1%2Cclass%3A%3A1%2Cclass%3A%3A0%2Cclass%3A%3A2&search_id=none&order_by=popularity&children_qty=2&languagecode=en-us&children_age=5%2C7&search_type=city&offset=0&dest_ids=-3712125&guest_qty=1&arrival_date=2019-08-13&departure_date=2019-08-15&room_qty=1";
            //q=1.0&from=SGD&to=MYR
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Key", "ca2d282acbmshdc149bd72e71e55p13aa6cjsn511e7a429b14");
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Host", "apidojo-booking-v1.p.rapidapi.com");
                var json = client.GetStringAsync(url);
                var propertyList = JsonConvert.DeserializeObject<PropertyListModel>(json.Result);

                return Json(new
                {
                    Properties = propertyList
                }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}