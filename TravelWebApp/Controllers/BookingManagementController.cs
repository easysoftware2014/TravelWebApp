using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
    //[Authorize]
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
        [HttpGet]
        public JsonResult GetPropertyList(string destination, string arrivalDate, string departureDate)
        {
            var propertyEndPoint = ConfigKeys.RapidApiBookingEndPoint();
            var url = propertyEndPoint +
                      "price_filter_currencycode=USD&" +
                      "travel_purpose=leisure&" +
                      "categories_filter=price%3A%3A9-40%2Cfree_cancellation%3A%3A1%2Cclass%3A%3A1%2Cclass%3A%3A0%2Cclass%3A%3A2&" +
                      "search_id=none&" +
                      "order_by=popularity&" +
                      "children_qty=2&" +
                      "languagecode=en-us&" +
                      "children_age=5%2C7&" +
                      "search_type=city&" +
                      "offset=0&" +
                      "dest_ids=" + GetDestinationId(destination)+"&" +
                      "guest_qty=1&" +
                      "arrival_date="+ arrivalDate + "&" +
                      "departure_date=" + departureDate + "&" +
                      "room_qty=1";
          
            var rapidApiKey = ConfigKeys.GetRapidApiKey();
            var rapidApiHost = ConfigKeys.GetRapidApiHost();
            
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Key", rapidApiKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Host", rapidApiHost);

                var json = client.GetStringAsync(url);
                var propertyList = JsonConvert.DeserializeObject<PropertyListModel>(json.Result);
                var properties = propertyList.Result != null
                    ? propertyList.Result.Where(x => x.MinimumPrice != "0") : propertyList.Result;

                return Json(new
                {
                    Properties = properties
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetFlightList()
        {
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        private string GetDestinationId(string destination)
        {
            var apiKey = ConfigKeys.GetRapidApiKey();
            var apiHost = ConfigKeys.GetRapidApiHost();
            var languageCode = "en-us";
            var place = destination;
            var endPoint = $"https://apidojo-booking-v1.p.rapidapi.com/locations/auto-complete?languagecode={languageCode}&text={place}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Key", apiKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Host", apiHost);

                var json = client.GetStringAsync(endPoint);
                var destinationModel = JsonConvert.DeserializeObject<List<DestinationModel>>(json.Result);

                return destinationModel?.First().DestinationId;

            }

            return string.Empty;
        }
    }
}