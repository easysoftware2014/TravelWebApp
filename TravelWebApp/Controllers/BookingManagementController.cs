using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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
        private readonly string _path = ConfigurationManager.AppSettings["Cities"];

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
                      "dest_ids=" + GetDestinationId(destination) + "&" +
                      "guest_qty=1&" +
                      "arrival_date=" + arrivalDate + "&" +
                      "departure_date=" + departureDate + "&" +
                      "room_qty=1";

            var rapidApiKey = ConfigKeys.GetRapidApiKey();
            var rapidApiHost = ConfigKeys.GetRapidPropertyApiHost();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Key", rapidApiKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Host", rapidApiHost);

                var json = client.GetStringAsync(url);
                var propertyList = JsonConvert.DeserializeObject<PropertyListModel>(json.Result);
                var properties = propertyList.Result != null
                    ? propertyList.Result.Where(x => x.MinimumPrice != "0") : propertyList.Result;

                var count = properties?.Count();
                //TempData["properties"] = propertyList.Result;
                //return RedirectToAction("PropertyResults", propertyList.Result);

                return Json(new
                {
                    Properties = properties,
                    Count = count
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetFlightList(string origin, string destination, string departure, string returnDate, string classType)
        {
            var rapidApiKey = ConfigKeys.GetRapidApiKey();
            var rapidApiHost = ConfigKeys.RapidApiFlightHost();
            var endPoint = ConfigKeys.RapidApiFlightEndPoint();
            var token = ConfigurationManager.AppSettings["FlightToken"];

            var url = endPoint +
                      "currency=zar&" +
                      "period_type=year&" +
                      "page=1&limit=30&" +
                      "show_to_affiliates=true&" +
                      "sorting=price&" +
                      "trip_class=0&" +
                      "token=" + token;


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Key", rapidApiKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Host", rapidApiHost);

                var json = client.GetStringAsync(url);
                var flightsModel = JsonConvert.DeserializeObject<FlightModel>(json.Result);
                var flights = flightsModel.data ?? flightsModel.data;

                return Json(new
                {
                    Flight = flights
                }, JsonRequestBehavior.AllowGet);
            }
        }

        private string GetIataCode(string name)
        {
            var path = _path;

            string jsonFromFile;
            using (var reader = new StreamReader(path))
            {
                jsonFromFile = reader.ReadToEnd();
            }

            var sName = name.Split(',')[0];
            var file = jsonFromFile;
            var serialized = JsonConvert.DeserializeObject<List<CitiesModel>>(file);
            var code = string.Empty;

            foreach (var cityModel in serialized)
            {
                if (cityModel.name.ToLower().Contains(sName.ToLower()))
                    code = cityModel.code;
            }

            return code;
        }
        public JsonResult GetCheapestFlightTicket(string from, string to, string departure, string returnDate)
        {
            var rapidApiKey = ConfigKeys.GetRapidApiKey();
            var rapidApiHost = ConfigKeys.RapidApiFlightHost();
            var endPoint = ConfigKeys.GetCheapestFlightTicket();
            var origin = GetIataCode(from);
            var destination = GetIataCode(to);
            var token = ConfigurationManager.AppSettings["FlightToken"];
            var returnD = returnDate.ToLower().Contains("undefined");

            if ((!string.IsNullOrEmpty(origin)) && (!string.IsNullOrEmpty(destination)))
            {
                var date = string.Empty;
                if (!returnD)
                    date = returnDate;

                endPoint = endPoint +
                           "origin=" + origin + "&" +
                           "destination=" + destination + "&" +
                           "depart_date=" + departure + "&" +
                           "return_date=" + date + "&" +
                           "token=" + token;

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Key", rapidApiKey);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Host", rapidApiHost);

                    var json = client.GetStringAsync(endPoint);
                    var cheapestFlightsModel = (json.Result);
                    //var flights = cheapestFlightsModel ?? cheapestFlightsModel;

                    return Json(new
                    {
                        Flight = cheapestFlightsModel
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new
            {
                Flight = (CheapestFlight[])null
            }, JsonRequestBehavior.AllowGet);
        }
        private string GetDestinationId(string destination)
        {
            var apiKey = ConfigKeys.GetRapidApiKey();
            var apiHost = ConfigKeys.GetRapidPropertyApiHost();
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
        public ActionResult PropertyResults(PropertyList[] list)
        {
            var model = (PropertyList[]) TempData["properties"];

            return View(model);
        }

        public ActionResult FlightResults(string results)
        {
            return View();
        }
    }
}